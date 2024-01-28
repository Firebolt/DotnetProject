using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class LoginRequest
    {
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
