using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Linq;
using System.Threading.Tasks;
using BirthdayBot.Database;

namespace BirthdayBot.Modules
{
    // Returns an embed listing known users/birthdays.
    public class list : ModuleBase<SocketCommandContext>
    {
        [Command("list")]
        public async Task BdayAsync()
        {

            var User = Context.User as SocketGuildUser;
            var role = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Captain");
            if (!User.Roles.Contains(role)) return;

            Database.Birthday[] birthdays = Data.Data.GetBirthdays();

            var builder = new EmbedBuilder()
                .WithTitle("Here are our teammates known birthdays!")
                .WithColor(new Color(0x8CE3C5))
                .WithThumbnailUrl("https://d2gg9evh47fn9z.cloudfront.net/800px_COLOURBOX3660468.jpg");

            foreach (Birthday bday in birthdays)
            {

                builder.Description += $"\n{Context.Client.GetUser(bday.UserId).Username} - {bday.Month}/{bday.Day}";
            }

            await ReplyAsync("", false, builder.Build());
        }
    }
}
