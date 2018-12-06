using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using System;
using System.Net.Http;
using System.Web.Script.Serialization;

namespace BirthdayBot.Modules.Streams
{
    public class UserStreaming
    {
        public static async Task UsersStreaming(DiscordSocketClient Client, HttpClient httpClient)
        {
            Console.WriteLine("UsersStreaming is running.");
            string url = "https://api.twitch.tv/kraken/streams?channel=larrycarryow,kadeoverwatch,kbearz_,emongg";
            var channel = Client.GetChannel(519034731321360384) as IMessageChannel;

            HttpResponseMessage response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var root = new JavaScriptSerializer().Deserialize<RootObject>(await response.Content.ReadAsStringAsync());
                if (root._total > 0)
                {
                    foreach (Stream stream in root.streams)
                    {
                        var builder = new EmbedBuilder()
                            .WithFooter(footer =>
                            {
                                footer
                                    .WithText($"Views: {stream.channel.views} - Followers: {stream.channel.followers} - Created by Kade");
                            })
                            .WithThumbnailUrl($"{stream.channel.logo}")
                            .WithImageUrl($"{stream.preview.medium}")
                            .WithAuthor(author =>
                            {
                                author
                                    .WithName($"{stream.channel.status}")
                                    .WithUrl($"{stream.channel.url}");
                            });
                        var embed = builder.Build();
                        await channel.SendMessageAsync($"{stream.channel.display_name} has gone live playing {stream.game}!", embed: embed).ConfigureAwait(false);
                    }
                } else
                {
                    await channel.SendMessageAsync("[Debugging] Nobody is currently streaming.");
                    Console.WriteLine("Stream checker ran, but nobody is live currently.");
                }
            } else
            {
                Console.WriteLine("Stream announcements failed.");
            }
        }
    }
}
