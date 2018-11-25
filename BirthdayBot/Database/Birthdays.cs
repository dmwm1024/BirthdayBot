using System.ComponentModel.DataAnnotations;

namespace BirthdayBot.Database
{
    // Used for migration/db instantiation for MSEF
    public class Birthday
    {
        [Key]
        public ulong UserId { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
    }
}
