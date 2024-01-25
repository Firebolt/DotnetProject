using FinalProject.Models;

namespace FinalProject.Services.Interfaces
{
    public interface ISeatService
    {
        Task<IEnumerable<Seat>> GetAllSeatsAsync();
        Task<Seat> GetSeatAsync(int flightId, string seatName);
        Task CreateSeatAsync(int flightId, string seatName);
        Task UpdateSeatAsync(int flightId, string seatName, bool isBooked);
        Task DeleteSeatAsync(int flightId, string seatName);
    }
}
