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
            System.Console.WriteLine($"Inside BdayAsync - Msg from: {User.Username}");

            var role = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Captain");
            if (!User.Roles.Contains(role)) System.Console.WriteLine($"{User.Username} is not a captain.");

            if (!User.Roles.Contains(role) && User.Username != "Kade") return;

            Database.Birthday[] birthdays = Data.Data.GetBirthdays();

            var builder = new EmbedBuilder()
                .WithTitle("Here are our teammates birthdays!")
                .WithColor(new Color(0x8CE3C5))
                .WithThumbnailUrl("https://d2gg9evh47fn9z.cloudfront.net/800px_COLOURBOX3660468.jpg");

            foreach (Birthday bday in birthdays)
            {   
                if (Context.Client.GetUser(bday.UserId) != null) builder.Description += $"\n{Context.Client.GetUser(bday.UserId).Username} - {bday.Month}/{bday.Day}";
            }
            builder.Description += $"\n*Message a captain if you want your birthday added!*";

            await ReplyAsync("", false, builder.Build());
        }
    }
}
