using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models.DTOs
{
    public class UserUpdateDTO
    {
        [Required(ErrorMessage = "The Name field is required.")]
        [StringLength(50, ErrorMessage = "The Nombre field cannot exceed 50 characters.")]
        public string Name { get; set; }

        [Required]
        public string role { get; set; } = "user";

        public string cv { get; set; }

        public string experience { get; set; }

        public string englishLevel { get; set; }
    }
}
