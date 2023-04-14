using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models
{
    public class Log
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LogId { get; set; } // primary key for database
        public int? UserId { get; set; } // nullable int for user id
        public string Method { get; set; } // string for method used
        public string Path { get; set; } // string for path
        public DateTime Date { get; set; } = DateTime.Now; // DateTime for current date time

    }
}
