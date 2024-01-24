using FinalProject.Models;
using FinalProject.Repositories.Interfaces;
using FinalProject.Services.Interfaces;

namespace FinalProject.Services.Implementations
{
    public class SeatService : ISeatService
    {
        private readonly IRepository<Seat> _seatRepository;

        public SeatService(IRepository<Seat> seatRepository)
        {
            _seatRepository = seatRepository;
        }

        public async Task<IEnumerable<Seat>> GetAllSeatsAsync()
        {
            return await _seatRepository.GetAllAsync();
        }

        public async Task<Seat> GetSeatByNameAsync(int flightId, string seatName)
        {
            return await _seatRepository.GetById(flightId, seatName);
        }

        public async Task CreateSeatAsync(int flightId, string seatName)
        {
            var newSeat = new Seat
            {
                FID = flightId,
                Name = seatName,
                Booked = false
            };

            await _seatRepository.Add(newSeat);
        }

        public async Task UpdateSeatAsync(int flightId, string seatName, bool isBooked)
        {
            var existingSeat = await _seatRepository.GetById(flightId, seatName);
            if (existingSeat != null)
            {
                existingSeat.Booked = isBooked;

                await _seatRepository.Update(existingSeat);
            }
        }

        public async Task DeleteSeatAsync(int flightId, string seatName)
        {
            await _seatRepository.Delete(flightId, seatName);
        }
    }
}

