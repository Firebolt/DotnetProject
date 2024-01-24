
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models

{
    public class Seat
    {
        public int FID { get; set; }

        [Key]
        public string? Name { get; set; }
        public bool Booked { get; set; }

        // Navigation property for the foreign key relationship
        [ForeignKey("FID")]
        public required Flight Flight { get; set; }
    }
}
