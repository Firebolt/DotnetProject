using FinalProject.Models;

namespace FinalProject.Services.Interfaces
{
    public interface ITicketService
    {
        Task<IEnumerable<Ticket>> GetAllTicketsAsync();
        Task<Ticket> GetTicketAsync(string userId, int flightId);
        Task<IEnumerable<Ticket>> GetTicketsAsync(string userId);
        Task CreateTicketAsync(string userId, int flightId, DateTime bookedDate, string seatNumber);
        Task UpdateTicketAsync(string userId, int flightId, DateTime bookedDate, string seatNumber);
        Task DeleteTicketAsync(string userId, int flightId);
    }
}
