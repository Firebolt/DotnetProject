using FinalProject.Models;

namespace FinalProject.Repositories.Interfaces
{
    public interface ITicketRepository
    {
        Task<Ticket> GetTicketAsync(int uid, int fid);
        Task<IEnumerable<Ticket>> GetAllTicketAsync();
        Task AddTicketAsync(Ticket ticket);
        Task UpdateTicketAsync(Ticket ticket);
        Task DeleteTicketAsync(int uid, int fid);
    
    }
}
