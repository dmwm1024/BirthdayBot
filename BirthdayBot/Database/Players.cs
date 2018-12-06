using System.ComponentModel.DataAnnotations;

namespace BirthdayBot.Database
{
    // Used for migration/db instantiation for MSEF
    public class Player
    {
        [Key]
        public ulong DiscordID { get; set; }
        public string BattleTag { get; set; }
        public ushort CompetitiveRank { get; set; }
    }
}
