using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace DB
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        [StringLength(50, ErrorMessage = "The Nombre field cannot exceed 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Password field is required.")]
        [MinLength(8, ErrorMessage = "The Password field must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", ErrorMessage = "The Password field must contain at least one uppercase letter, one lowercase letter, and one digit.")]
        public string Password { get; set; }


        [Required]
        [Range(0, long.MaxValue, ErrorMessage = "The Balance field must be a positive long integer.")]
        public long Balance { get; set; } = 0;

        public ICollection<Operation> ReceivedOperations { get; set; } = new List<Operation>();
        public ICollection<Operation> SendedOperations { get; set; } = new List<Operation>();

        public void HashPassword()
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(Password));
            Password = Convert.ToBase64String(hashedBytes);
        }
    }
}
