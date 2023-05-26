using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace fplWagerApi.Models
{
    [Table("wagers")]
    public class Wagers
    {
        [Key]
        [Column("wager_id")]
        public int WagerId { get; set; }

        [Column("wager_name")]
        public string WagerName { get; set; }

        [Column("entry_token")]
        public int EntryToken { get; set; }

        [Column("token_distribution")]
        public string TokenDistribution { get; set; }

        [Column("win_type")]
        public string WinType { get; set; }

        [Column("created_by")]
        public int CreatedBy { get; set; }

        [Column("gameweek")]
        public int Gameweek { get; set; }

        [Column("is_started")]
        public bool IsStarted { get; set; }

        [Column("is_completed")]
        public bool IsCompleted { get; set; }

        [Column("date_created")]
        public DateTime DateCreated { get; set; } = DateTime.Now;


    }
}
