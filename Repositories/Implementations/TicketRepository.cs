using FinalProject.Models;
using FinalProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Repositories.Implementations
{
    public class TicketRepository : ITicketRepository
    {
        private readonly AppDbContext _context;

        public TicketRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Ticket> GetTicketAsync(int uid, int fid)
        {
            return await _context.Tickets
                .FirstOrDefaultAsync(t => t.UID == uid && t.FID == fid);
        }

        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
        {
            return await _context.Tickets.ToListAsync();
        }

        public async Task AddTicketAsync(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTicketAsync(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTicketAsync(int uid, int fid)
        {
            var ticket = await GetTicketAsync(uid, fid);
            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
                await _context.SaveChangesAsync();
            }
        }

        public Task<IEnumerable<Ticket>> GetAllTicketAsync()
        {
            throw new NotImplementedException();
        }
    }
}

    

