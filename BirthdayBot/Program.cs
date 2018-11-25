using BirthdayBot.Modules;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace BirthdayBot
{
    class Program
    {
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;
        private System.Threading.Timer timer;


        public async Task RunBotAsync()
        {
            Console.WriteLine(Assembly.GetEntryAssembly().Location);
            _client = new DiscordSocketClient();
            _commands = new CommandService();

            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            string botToken = "token is secret";

            _client.Log += Log;

            await RegisterCommandsAsync();
            await _client.LoginAsync(Discord.TokenType.Bot, botToken);
            await _client.StartAsync();

            timer = new System.Threading.Timer(TimedAnnouncement, null, 0, 1000 * 60 * 60 * 24); // 24 hour interval

            await Task.Delay(-1);
        }

        public async void TimedAnnouncement(object state)
        {
            await Announce.AnnounceBirthdays(_client);
        }

        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        public async Task RegisterCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;

            if (message is null || message.Author.IsBot) return;

            int argPos = 0;

            if (message.HasStringPrefix("bday!", ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos)) {
                var context = new SocketCommandContext(_client, message);
                var result = await _commands.ExecuteAsync(context, argPos, _services);

                if (!result.IsSuccess) Console.WriteLine(result.ErrorReason);
            }
        }
    }
}
