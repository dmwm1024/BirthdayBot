using BirthdayBot.Modules;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
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

        // Core Instantiation 
        public async Task RunBotAsync()
        {
            Console.WriteLine(Assembly.GetEntryAssembly().Location);
            _client = new DiscordSocketClient();
            _commands = new CommandService();

            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            string botToken = "token is secret nerds";


            _client.Log += Log;

            await RegisterCommandsAsync();
            await _client.LoginAsync(Discord.TokenType.Bot, botToken);
            await _client.StartAsync();

            timer = new System.Threading.Timer(TimedAnnouncement, null, 0, 1000 * 60 * 60 * 1); // 24 hour interval

            await Task.Delay(-1); // Simply prevents the bot from terminating.
        }


        // Triggers the daily check/announcement of any existing birthdays.
        public async void TimedAnnouncement(object state)
        {
            if (DateTime.Now.Hour == 11) await Announce.AnnounceBirthdays(_client);
        }

        // Crude logging for dev. File-based logging will be added if I care enough.
        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        // These 2 use the Discord API dependency injection to load our modules (commands) and event hooks.
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

            if (message.HasStringPrefix("bday!", ref argPos) || message.HasStringPrefix("kbot!", ref argPos) || message.HasStringPrefix("Kbot!", ref argPos))
            {
                Console.WriteLine("Command found");
                var context = new SocketCommandContext(_client, message);
                var result = await _commands.ExecuteAsync(context, argPos, _services);

                if (!result.IsSuccess) Console.WriteLine(result.ErrorReason);
            }
            else
            {
                if (message.Content.Contains("516226214474809355"))
                {
                    var embed = new EmbedBuilder();
                    string msgUpper = message.Content.ToUpper();

                    var randomGifs = new List<string>
                {
                    "https://78.media.tumblr.com/4bd7583ca8f43597bb74b199158e4c08/tumblr_inline_nk94ozI7081rrrkde.gif",
                    "http://i.imgur.com/7Q5WUHB.gif",
                    "https://i.pinimg.com/originals/65/7a/c9/657ac95dc8daca1808a19be2e100928a.gif",
                    "http://i.imgur.com/RtSTvaM.gif",
                    "https://img.buzzfeed.com/buzzfeed-static/static/2015-01/27/7/enhanced/webdr10/anigif_original-5801-1422361310-10.gif",
                    "http://24.media.tumblr.com/tumblr_ldiwwtY5m01qbrrwmo1_500.gif",
                    "https://data.whicdn.com/images/156929664/original.gif"
                };

                    if (msgUpper.Contains("THANK YOU"))
                    {
                        embed.WithDescription("You're welcome.");
                        embed.WithImageUrl("https://78.media.tumblr.com/4bd7583ca8f43597bb74b199158e4c08/tumblr_inline_nk94ozI7081rrrkde.gif");
                    }
                    else if (msgUpper.Contains("LOST") && msgUpper.Contains("COMP") && !msgUpper.Contains("almost"))
                    {
                        embed.WithDescription("I still love you.");
                        embed.WithImageUrl("http://blog.tvtime.com/wp-content/uploads/2017/11/rachel-green-gif.gif");
                    }
                    else if (msgUpper.Contains("GOOD") && msgUpper.Contains("NIGHT"))
                    {
                        embed.WithDescription("Sleep well.");
                        embed.WithImageUrl("https://i.ytimg.com/vi/YNaaX9qQ2Mw/maxresdefault.jpg");
                    }
                    else if (msgUpper.Contains("GOOD") && msgUpper.Contains("MORNING"))
                    {
                        embed.WithDescription("Good morning!");
                        embed.WithImageUrl("https://keyassets-p2.timeincuk.net/wp/prod/wp-content/uploads/sites/30/2016/04/5y0EGX.gif");
                    }
                    else
                    {
                        Random rand = new Random();
                        int index = rand.Next(randomGifs.Count);
                        embed.WithImageUrl(randomGifs[index]);
                    }

                    await message.Channel.SendMessageAsync("", false, embed.Build());
                }
            }
        }
    }
}
