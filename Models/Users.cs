using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace fplWagerApi.Models
{
    [Table("users")]
    public class Users
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("fpl_id")]
        public int FplId { get; set; }

        [Column("fullname")]
        public string FullName { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("tokens")]
        public int Tokens { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("salt")]
        public string Salt { get; set; }

        [Column("date_created")]
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
