using System.ComponentModel.DataAnnotations;

namespace OperationalAdministrator.Models
{
    public class AuthRequest
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string password { get; set; }
    }
}
