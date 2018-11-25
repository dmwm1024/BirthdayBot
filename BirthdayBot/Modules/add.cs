using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Linq;
using System.Threading.Tasks;
using BirthdayBot.Data;

namespace BirthdayBot.Modules
{
    public class add : ModuleBase<SocketCommandContext>
    {
        [Command("add")]
        public async Task BdayAsync(IUser BirthdayUser, int Month, int Day)
        {

            var User = Context.User as SocketGuildUser;
            var role = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Captain");
            if (!User.Roles.Contains(role)) return;

            await Data.Data.SaveBirthday(BirthdayUser.Id, Month, Day);

            /*
            EmbedBuilder builder = new EmbedBuilder();

            builder.WithTitle("Birthday!")
                .WithDescription("Be sure to wish a very happy birthday to these users!")
                .WithColor(Color.Blue);

            await ReplyAsync("", false, builder.Build());
            */

            await ReplyAsync("Completion message.");
        }
    }
}
