using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models
{
    public class User : IdentityUser
    {
        [Required]
        public Role UserRole { get; set; }
    }
    public enum Role
    {
        Passenger, TicketAgent, Administrator
    }
}
