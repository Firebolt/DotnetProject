using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class Ticket
    {
        //please add required annotation to the models
        [Key]
        public int UID { get; set; }
        [Key]
        public int FID { get; set; }
        public DateTime BookedDate { get; set; }
        public string? SeatNumber { get; set; }

    }
}
