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
            //string DbLocation = Assembly.GetEntryAssembly().Location.Replace(@"bin\Debug\BirthdayBot.exe", @"Data\");
            string DbLocation = @"C:\Users\Kade\source\repos\BirthdayBot\BirthdayBot\Data\Database.sqlite";
            Options.UseSqlite($"Data Source={DbLocation}");
        }
    }
}
