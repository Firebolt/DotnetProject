using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class LoginRequest
    {
        [Required,DataType(DataType.Password)]
        public required string Password { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        public string? Username { get; set; }
        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
