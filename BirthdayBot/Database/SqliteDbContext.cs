using Microsoft.EntityFrameworkCore;

namespace BirthdayBot.Database
{
    // Crude setup for the data connection. Obviously need to rewrite this for portability.
    public class SqliteDbContext : DbContext
    {
        public DbSet<Birthday> Birthdays { get; set; }
        public DbSet<Player> Players { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder Options)
        {
            // Yes, I'm lazy. I'll rewrite this for portability later. Don't @ me.
            string DbLocation = @"C:\Users\Kade\source\repos\BirthdayBot\BirthdayBot\Data\Database.sqlite";
            Options.UseSqlite($"Data Source={DbLocation}");
        }
    }
}
