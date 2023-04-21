using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.DTOs
{
    public class AccountDTO
    {
        [Required]
        public string AccountName { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        public string OperationManagerName { get; set; }

        [Required]
        public int TeamId { get; set; }
    }
}
