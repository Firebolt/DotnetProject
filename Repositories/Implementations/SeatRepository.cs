using FinalProject.Models;
using FinalProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Repositories.Implementations
{
    public class SeatRepository : ISeatRepository
    {
        private readonly AppDbContext _context;

        public SeatRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Seat> GetSeatAsync(int fid, string name)
        {
            return await _context.Seats
                .FirstOrDefaultAsync(s => s.Name == name && s.FID == fid);
        }
        public async Task<IEnumerable<Seat>> GetSeatsAsync(int fid)
        {
            return await _context.Seats
                .Where(s => s.FID == fid).ToListAsync();
        }
        public async Task<IEnumerable<Seat>>GetAllSeatsAsync()
        {

            return await _context.Seats.ToListAsync();
        }
        public async Task AddSeatAsync(Seat seat)
        {
            _context.Seats.Add(seat);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateSeatAsync(Seat seat)
        {
            _context.Seats.Update(seat);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteSeatAsync(int fid, string name)
        {
            var seat = await GetSeatAsync(fid, name);
            if (seat != null)
            {
                _context.Seats.Remove(seat);
                await _context.SaveChangesAsync();
            }
           
        }
    }
}
