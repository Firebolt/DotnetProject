using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class FlightRequest
    {   
        public int NumofRows { get; set; }
        [Required, DataType(DataType.Currency)]
        public decimal TicketCost { get; set; }
        [Required, DataType(DataType.DateTime)]
        public DateTimeOffset DeparDateandTimeOffset { get; set; }
        [Required, DataType(DataType.DateTime)]
        public DateTimeOffset ArrDateandTimeOffset { get; set; }
        [Required]
        public string? Destination { get; set; }
        [Required]
        public string? TakeOffLocation { get; set; }
        [Required, DataType(DataType.DateTime)]
        public TimeSpan FlightDuration { get; set; }
        [Required]
        public string? AirportLoc { get; set; }
    }
}
