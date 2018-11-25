using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayBot.Database
{
    public class SqliteDbContext : DbContext
    {
        public DbSet<Birthday> Birthdays { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder Options)
        {
            // Yes, I'm lazy. I'll rewrite this for portability later. Don't @ me.
            string DbLocation = @"C:\Users\Kade\source\repos\BirthdayBot\BirthdayBot\Data\Database.sqlite";
            Options.UseSqlite($"Data Source={DbLocation}");
        }
    }
}
