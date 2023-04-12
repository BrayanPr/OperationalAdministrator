using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class Operation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OperationId { get; set; }

        public bool OperationStatus { get; set; }

        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        public virtual User Sender { get; set; }

        public virtual User Receiver { get; set; }

    }
}
