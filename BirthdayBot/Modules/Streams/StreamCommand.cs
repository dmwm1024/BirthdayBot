using BirthdayBot.Modules.Streams;
using Discord.Commands;
using Discord.WebSocket;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BirthdayBot.Modules
{
    // Fail safe in case for whatever reason the bot doesn't do it... Unlikely. 
    // Uses the Announce class.
    public class streamCommand : ModuleBase<SocketCommandContext>
    {
        HttpClient httpClient = new HttpClient();

        [Command("streamChecker")]
        public async Task StreamCommand()
        {
            ReplyAsync("This is currently being developed, Kade will let you know when it is ready :).");
            return;
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("client-ID", "1l414vueqcvvhv4qk5ttjxwzruet1a");
            await UserStreaming.UsersStreaming(Context.Client, httpClient);
        }
    }
}

