using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FinalProject.Models
{
   public class Flight
   {
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      [Required]
      public int FID{ get; set; }
      [Required]
      public int NumofRows { get; set; }
      [Required,DataType(DataType.Currency), DisplayName("Ticket Cost")]
      public decimal TicketCost { get; set; }
      [Required, DataType(DataType.DateTime), DisplayName("Departure Time")]
      public DateTimeOffset DeparDateandTimeOffset { get; set; }
      [Required, DataType(DataType.DateTime), DisplayName("Arrival Time")]
      public DateTimeOffset ArrDateandTimeOffset { get; set; }
      [Required]
      public required string Destination { get; set; }
      [Required, DisplayName("Takeoff Location")]
      public required string TakeOffLocation { get; set; }
      [Required,DataType(DataType.Time), DisplayName("Flight Duration")]
      public TimeSpan FlightDuration { get; set; }
      [Required, DisplayName("Airport Location")]
      public required string AirportLoc { get; set; }
   }
}
