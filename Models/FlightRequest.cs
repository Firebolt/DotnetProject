namespace FinalProject.Models
{
    public class FlightRequest
    {
        public int NumofSeat { get; set; }
        public decimal TicketCost { get; set; }
        public DateTimeOffset DeparDateandTimeOffset { get; set; }
        public DateTimeOffset ArrDateandTimeOffset { get; set; }
        public string? Destination { get; set; }
        public string? TakeOffLocation { get; set; }
        public TimeSpan FlightDuration { get; set; }
        public string? AirportLoc { get; set; }
    }
}
