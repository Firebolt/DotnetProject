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
        public async Task<Seat> GetSeatAsync(string name, int fid)
        {
            return await _context.Seats
                .FirstOrDefaultAsync(s => s.Name == name && s.FID == fid);
        }
        public async Task<IEnumerable<Seat>>GetAllSeatsAsyn()
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
        public async Task DeleteSeatAsync(string name, int fid)
        {
            var seat = await GetSeatAsync(name, fid);
            if (seat != null)
            {
                _context.Seats.Remove(seat);
                await _context.SaveChangesAsync();
            }
           
        }

        public Task<Seat> GetSeatAsync(int fid, string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Seat>> GetAllSeatsAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteSeatAsync(int fid, string name)
        {
            throw new NotImplementedException();
        }
    }
}
