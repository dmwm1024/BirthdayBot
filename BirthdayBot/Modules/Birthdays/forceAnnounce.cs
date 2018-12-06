using Discord.Commands;
using Discord.WebSocket;
using System.Linq;
using System.Threading.Tasks;

namespace BirthdayBot.Modules
{
    // Fail safe in case for whatever reason the bot doesn't do it... Unlikely. 
    // Uses the Announce class.
    public class forceAnnounce : ModuleBase<SocketCommandContext>
    {
        [Command("forceAnnounce")]
        public async Task BdayAsync()
        {

            var User = Context.User as SocketGuildUser;
            var role = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Captain");
            if (!User.Roles.Contains(role) && User.Username != "Kade") return;

            await Announce.AnnounceBirthdays(Context.Client);
        }
    }
}
