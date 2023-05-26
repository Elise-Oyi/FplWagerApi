using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FplWagerApi.Models
{
    [Table("wagerlist")]
    public class WagerList
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("wager_id")]
        public int WagerId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("fpl_id")]
        public int FplId { get; set; }
    }
}

//{
//    "wagername": "test2",
//    "entrytoken":10,
//    "tokendistribution":"top 1",
//    "wintype":"weekly",
//    "createdby": 2,
//    "gameweek":38,
//    "isstarted":false,
//    "iscompleted":false
//}