using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UID { get; set; }
        [Required,DataType(DataType.EmailAddress)]
        public required string Email { get; set; }
        [Required]
        public required  string Username { get; set; }
        [Required,DataType(DataType.Password)]
        public required string Password { get; set; }
        [Required]
        public Role UserRole { get; set; }

    }
    public enum Role
    {
        Passenger, TicketAgent, Administrator
    }

}
