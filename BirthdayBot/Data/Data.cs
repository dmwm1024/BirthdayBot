using BirthdayBot.Database;
using System.Linq;
using System.Threading.Tasks;

namespace BirthdayBot.Data
{
    public static class Data
    {
        // Gather an array of the birthdays on record (Uses MS Entity Framework)
        public static Birthday[] GetBirthdays()
        {
            using (var DbContext = new SqliteDbContext())
            {
                Birthday[] bdays = DbContext.Birthdays.OrderBy(e => e.Month).ThenBy(e => e.Day).ToArray();
                return bdays;
            }
        } 

        // Method for saving/updating records
        public static async Task SaveBirthday(ulong UserId, int Month, int Day)
        {
            using (var DbContext = new SqliteDbContext())
            {
                if (DbContext.Birthdays.Where(x => x.UserId == UserId).Count() < 1)
                {
                    // User doesn't exist - Create
                    DbContext.Birthdays.Add(new Birthday
                    {
                        UserId = UserId,
                        Month = Month,
                        Day = Day
                    });
                } else
                {
                    // User already exists - Update
                    Birthday Current = DbContext.Birthdays.Where(x => x.UserId == UserId).FirstOrDefault();
                    Current.Month = Month;
                    Current.Day = Day;
                    DbContext.Birthdays.Update(Current);
                }
                await DbContext.SaveChangesAsync();
            }
        } 
    }
}
