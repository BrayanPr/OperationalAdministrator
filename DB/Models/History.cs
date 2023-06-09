﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models
{
    public class History
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HistoryId { get; set; }


        public DateTime date { get; set; } = DateTime.Now;

        public int NewTeam { get; set; }

        public int? OldTeam { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

    }
}
