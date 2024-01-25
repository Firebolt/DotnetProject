
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FinalProject.Models

{
    public class Seat
    {

        [Key]
        [Required]
        public string? Name { get; set; }
        [DefaultValue(false)]
        public bool Booked { get; set; }

        // Navigation property for the foreign key relationship
        [ForeignKey("FID")]
        public int FID { get; set; }
        public virtual required Flight Flight { get; set; }
    }
}
