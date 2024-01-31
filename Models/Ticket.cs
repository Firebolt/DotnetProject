using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class Ticket
    {
        [Key, Required]
        public required string UID { get; set; }
        [Key, Required, DisplayName("Flight Number")]
        public int FID { get; set; }
        [Required, DataType(DataType.Date), DisplayName("Booked Date")]
        public DateTime BookedDate { get; set; }
        [Required, DisplayName("Seat Number")]
        public string? SeatNumber { get; set; }
    }
}
