using FinalProject.Models;
using FinalProject.Repositories.Interfaces;
using FinalProject.Services.Interfaces;

namespace FinalProject.Services.Implementations
{
    public class TicketService : ITicketService
    {
        private readonly IRepository<Ticket> _ticketRepository;

        public TicketService(IRepository<Ticket> ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
        {
            return await _ticketRepository.GetAllAsync();
        }

        public async Task<Ticket> GetTicketByIdAsync(int userId, int flightId)
        {
            return await _ticketRepository.GetById(userId, flightId);
        }

        public async Task CreateTicketAsync(int userId, int flightId, DateTime bookedDate, string seatNumber)
        {
            var newTicket = new Ticket
            {
                UID = userId,
                FID = flightId,
                BookedDate = bookedDate,
                SeatNumber = seatNumber
            };

            await _ticketRepository.Add(newTicket);
        }

        public async Task UpdateTicketAsync(int userId, int flightId, DateTime bookedDate, string seatNumber)
        {
            var existingTicket = await _ticketRepository.GetById(userId, flightId);
            if (existingTicket != null)
            {
                existingTicket.BookedDate = bookedDate;
                existingTicket.SeatNumber = seatNumber;

                await _ticketRepository.Update(existingTicket);
            }
        }

        public async Task DeleteTicketAsync(int userId, int flightId)
        {
            await _ticketRepository.Delete(userId, flightId);
        }
    }
}

