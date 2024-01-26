using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class Ticket
    {
        [Key]
        [Required]
        public int UID { get; set; }
        [Key]
        [Required]
        public int FID { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime BookedDate { get; set; }
        [Required]
        public string? SeatNumber { get; set; }
    }
}
