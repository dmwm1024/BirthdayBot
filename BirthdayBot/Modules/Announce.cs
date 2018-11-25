using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Linq;
using System.Threading.Tasks;
using BirthdayBot.Data;
using BirthdayBot.Database;
using System;

namespace BirthdayBot.Modules
{
    public class Announce
    {
        public static Task AnnounceBirthdays(DiscordSocketClient Client)
        {
            Console.WriteLine($"Checking Today's Birthdays: {DateTime.Now.ToString()}");

            Database.Birthday[] birthdays = Data.Data.GetBirthdays();
            var builder = new EmbedBuilder()
                .WithTitle("Here are our teammates known birthdays!")
                .WithColor(new Color(0x8CE3C5))
                .WithThumbnailUrl("https://d2gg9evh47fn9z.cloudfront.net/800px_COLOURBOX3660468.jpg");

            int numOfBirthdays = 0;
            DateTime today = DateTime.Now;

            foreach (Birthday bday in birthdays)
            {
                if (bday.Month == today.Month && bday.Day == today.Day)
                {
                    numOfBirthdays++;
                    builder.Description += $"\n{Client.GetUser(bday.UserId).Username} - {bday.Month}/{bday.Day}";
                }
            }

            if (numOfBirthdays > 0)
            {
                var channel = Client.GetChannel(469899144194949123) as IMessageChannel;
                channel.SendMessageAsync("", false, builder.Build());
            }
            return Task.CompletedTask;
        }
    }
}
