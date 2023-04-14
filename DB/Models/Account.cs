
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB.Models
{

    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountId { get; set; }
        [Required]
        public string AccountName { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        public string OperationManagerName { get; set; }

        [Required]
        public int TeamId { get; set; }

        public virtual Team Team { get; set; }
    }
}
