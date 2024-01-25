using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class UserRequest
    {
        [Required]
        public string? Username { get; set; }
        [Required,DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required]
        public Role UserRole { get; set; }
    }
}
