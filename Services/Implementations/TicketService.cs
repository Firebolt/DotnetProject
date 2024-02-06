using FinalProject.Models;
using FinalProject.Repositories.Interfaces;
using FinalProject.Services.Interfaces;

namespace FinalProject.Services.Implementations
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserService _userService;
        private readonly IFlightService _flightService;
        public TicketService(ITicketRepository ticketRepository, IUserService userService, IFlightService flightService)
        {
            _ticketRepository = ticketRepository;
            _userService = userService;
            _flightService = flightService;
        }

        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
        {
            return await _ticketRepository.GetAllTicketAsync();
        }

        public async Task<Ticket> GetTicketAsync(string userId, int flightId)
        {
            return await _ticketRepository.GetTicketAsync(userId, flightId);
        }

        public async Task<IEnumerable<Ticket>> GetTicketsAsync(string userId)
        {
            return await _ticketRepository.GetTicketsAsync(userId);
        }

        public async Task CreateTicketAsync(string userId, int flightId, DateTime bookedDate, string seatNumber)
        {
            var newTicket = new Ticket
            {
                UID = userId,
                user = await _userService.GetUserByIdAsync(userId),
                FID = flightId,
                Flight = await _flightService.GetFlightByIdAsync(flightId),
                BookedDate = bookedDate,
                SeatNumber = seatNumber
            };

            await _ticketRepository.AddTicketAsync(newTicket);
        }

        public async Task UpdateTicketAsync(string userId, int flightId, DateTime bookedDate, string seatNumber)
        {
            var existingTicket = await _ticketRepository.GetTicketAsync(userId, flightId);
            if (existingTicket != null)
            {
                existingTicket.BookedDate = bookedDate;
                existingTicket.SeatNumber = seatNumber;

                await _ticketRepository.UpdateTicketAsync(existingTicket);
            }
        }

        public async Task DeleteTicketAsync(string userId, int flightId)
        {
            await _ticketRepository.DeleteTicketAsync(userId, flightId);
        }
    }
}

