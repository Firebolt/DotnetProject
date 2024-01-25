using FinalProject.Models;

namespace FinalProject.Repositories.Interfaces
{
    public interface ISeatRepository
    {
        Task<Seat> GetSeatAsync(int fid, string seatName);
        Task<IEnumerable<Seat>> GetAllSeatsAsync();
        Task AddSeatAsync (Seat seat);
        Task UpdateSeatAsync(Seat seat);
        Task DeleteSeatAsync(int fid, string seatName );

    }
}
