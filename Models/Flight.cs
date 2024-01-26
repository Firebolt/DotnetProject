using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
   public class Flight
   {
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      [Required]
      public int FID{ get; set; }
      [Required]
      public int NumofSeat { get; set; }
      [Required,DataType(DataType.Currency)]
      public decimal TicketCost { get; set; }
      [Required, DataType(DataType.DateTime)]
      public DateTimeOffset DeparDateandTimeOffset { get; set; }
      [Required, DataType(DataType.DateTime)]
      public DateTimeOffset ArrDateandTimeOffset { get; set; }
      [Required]
      public required string Destination { get; set; }
      [Required]
      public required string TakeOffLocation { get; set; }
      [Required,DataType(DataType.Time)]
      public TimeSpan FlightDuration { get; set; }
      [Required]
      public required string AirportLoc { get; set; }
   }
}
