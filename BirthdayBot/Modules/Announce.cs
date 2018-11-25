using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;
using BirthdayBot.Database;
using System;

namespace BirthdayBot.Modules
{
    // This class is used for the timed (daily) birthday announcements.
    // It is also used for the forceAnnouncement command.
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

            // No announcement will be made if there are no birthdays.
            if (numOfBirthdays > 0)
            {
                var channel = Client.GetChannel(469899144194949123) as IMessageChannel;
                channel.SendMessageAsync("", false, builder.Build());
            }
            return Task.CompletedTask;
        }
    }
}
