using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Linq;
using System.Threading.Tasks;
using BirthdayBot.Data;
using BirthdayBot.Database;

namespace BirthdayBot.Modules
{
    public class forceAnnounce : ModuleBase<SocketCommandContext>
    {
        [Command("check")]
        public async Task BdayAsync()
        {

            var User = Context.User as SocketGuildUser;
            var role = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Captain");
            if (!User.Roles.Contains(role)) return;

            await Announce.AnnounceBirthdays(Context.Client);
        }
    }
}
