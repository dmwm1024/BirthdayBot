using Discord.Commands;
using System.Threading.Tasks;
using OverwatchAPI;
using System.Web.Script.Serialization;

namespace BirthdayBot.Modules
{
    public class getSR : ModuleBase<SocketCommandContext>
    {
        [Command("getSR")]
        public async Task GetSRAsync(string btag)
        {
                using (var owClient = new OverwatchClient())
                {
                    Player player = await owClient.GetPlayerAsync(btag);
                    if (player.IsProfilePrivate)
                    {
                        await ReplyAsync("Their profile is private or they have not played competitive this season.");
                    }
                    else
                    {
                    if (btag == "Garry#12914")
                        {
                            await ReplyAsync($"```{player.Username}\nSR: 4999. GitGud, nerds. (It's Actually: {player.CompetitiveRank})\nLevel: {player.PlayerLevel}\nEndorsement: {player.EndorsementLevel})```");
                        } else
                        {
                            await ReplyAsync($"```{player.Username}\nSR: {player.CompetitiveRank}\nLevel: {player.PlayerLevel}\nEndorsement: {player.EndorsementLevel}```");
                        }
                    }
                }
        }
    }
}
