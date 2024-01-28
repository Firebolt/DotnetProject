using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProject.Models
{
    public class UserRequest
    {
        [Required]
        public required string Username { get; set; }
        [Required,DataType(DataType.Password)]
        public required string Password { get; set; }
        [DataType(DataType.Password), Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public required string Email { get; set; }
        [Required, Display(Name = "User Role")]
        public Role UserRole { get; set; }
        public IEnumerable<SelectListItem>? UserRoleList { get; set; }
        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
