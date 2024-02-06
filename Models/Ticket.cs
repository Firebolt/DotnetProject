using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models
{
    public class Ticket
    {
        [Key, Required, ForeignKey("user")]
        public required string UID { get; set; }
        public virtual required User user { get; set; }
        [Key, Required, DisplayName("Flight Number"), ForeignKey("flight")]
        public int FID { get; set; }
        public virtual required Flight Flight { get; set; }
        [Key, Required, DisplayName("Seat Number")]
        public required string SeatNumber { get; set; }
        [Required, DataType(DataType.Date), DisplayName("Booked Date")]
        public DateTime BookedDate { get; set; }
    }
}
