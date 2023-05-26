using fplWagerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FplWagerApi.Models.DTO
{

    public class WagerListDto
    {
        public int WagerId { get; set; }
        public string WagerName { get; set; }
        public int EntryToken { get; set; }
        public string TokenDistribution { get; set; }
        public string WinType { get; set; }
        public int CreatedBy { get; set; }
        public int Gameweek { get; set; }
        public bool IsStarted { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DateCreated { get; set; }

        public int UserId { get; set; }
        public int FplId { get; set; }
    }
}
