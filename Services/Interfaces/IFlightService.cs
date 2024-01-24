using FinalProject.Models;

namespace FinalProject.Services.Interfaces
{
    public interface IFlightService
    {
        Task<IEnumerable<Flight>> GetAllFlightsAsync();
        Task<Flight> GetFlightByIdAsync(int flightId);
        Task CreateFlightAsync(FlightRequest flightRequest);
        Task UpdateFlightAsync(int flightId, FlightRequest updatedFlightRequest);
        Task DeleteFlightAsync(int flightId);
    }
}
