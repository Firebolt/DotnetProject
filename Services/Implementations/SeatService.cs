using FinalProject.Models;
using FinalProject.Repositories.Interfaces;
using FinalProject.Services.Interfaces;

namespace FinalProject.Services.Implementations
{
    public class SeatService : ISeatService
    {
        private readonly ISeatRepository _seatRepository;
        private readonly IRepository<Flight> _flight;

        public SeatService(ISeatRepository seatRepository, IRepository<Flight> flight)
        {
            _seatRepository = seatRepository;
            _flight = flight;
        }

        public async Task<IEnumerable<Seat>> GetAllSeatsAsync()
        {
            return await _seatRepository.GetAllSeatsAsync();
        }

        public async Task<Seat> GetSeatAsync(int flightId, string seatName)
        {
            return await _seatRepository.GetSeatAsync(flightId, seatName);
        }

        public async Task CreateSeatAsync(int flightId, string seatName)
        {
            var newSeat = new Seat
            {
                FID = flightId,
                Name = seatName,
                Booked = false,
                Flight = await _flight.GetById(flightId)
            };

            await _seatRepository.AddSeatAsync(newSeat);
        }

        public async Task UpdateSeatAsync(int flightId, string seatName, bool isBooked)
        {
            var existingSeat = await _seatRepository.GetSeatAsync(flightId, seatName);
            if (existingSeat != null)
            {
                existingSeat.Booked = isBooked;

                await _seatRepository.UpdateSeatAsync(existingSeat);
            }
        }

        public async Task DeleteSeatAsync(int flightId, string seatName)
        {
            await _seatRepository.DeleteSeatAsync(flightId, seatName);
        }
    }
}

