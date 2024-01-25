using FinalProject.Models;

namespace FinalProject.Services.Interfaces
{
    public interface ITicketService
    {

        Task<IEnumerable<Ticket>> GetAllTicketsAsync();
        Task<Ticket> GetTicketAsync(int userId, int flightId);
        Task CreateTicketAsync(int userId, int flightId, DateTime bookedDate, string seatNumber);
        Task UpdateTicketAsync(int userId, int flightId, DateTime bookedDate, string seatNumber);
        Task DeleteTicketAsync(int userId, int flightId);

    }
}
