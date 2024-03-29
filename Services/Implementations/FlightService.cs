﻿using FinalProject.Models;
using FinalProject.Repositories.Interfaces;
using FinalProject.Services.Interfaces;

namespace FinalProject.Services.Implementations
{
    public class FlightService : IFlightService
    {
        private readonly IRepository<Flight> _flightRepository;

        public FlightService(IRepository<Flight> flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public async Task<IEnumerable<Flight>> GetAllFlightsAsync()
        {
            return await _flightRepository.GetAllAsync();
        }

        public async Task<Flight> GetFlightByIdAsync(int flightId)
        {
            return await _flightRepository.GetById(flightId);
        }

        public async Task<int> CreateFlightAsync(FlightRequest flightRequest)
        {
            var newFlight = new Flight
            {
                NumofRows = flightRequest.NumofRows,
                TicketCost = flightRequest.TicketCost,
                DeparDateandTimeOffset = flightRequest.DeparDateandTimeOffset.ToUniversalTime(),
                ArrDateandTimeOffset = flightRequest.ArrDateandTimeOffset.ToUniversalTime(),
                Destination = flightRequest.Destination,
                TakeOffLocation = flightRequest.TakeOffLocation,
                FlightDuration = flightRequest.FlightDuration,
                AirportLoc = flightRequest.AirportLoc
            };
            await _flightRepository.Add(newFlight);
            return newFlight.FID;
        }

        public async Task UpdateFlightAsync(int flightId, FlightRequest updatedFlightRequest)
        {
            var existingFlight = await _flightRepository.GetById(flightId);
            if (existingFlight != null)
            {
                existingFlight.NumofRows = updatedFlightRequest.NumofRows;
                existingFlight.TicketCost = updatedFlightRequest.TicketCost;
                existingFlight.DeparDateandTimeOffset = updatedFlightRequest.DeparDateandTimeOffset;
                existingFlight.ArrDateandTimeOffset = updatedFlightRequest.ArrDateandTimeOffset;
                existingFlight.Destination = updatedFlightRequest.Destination;
                existingFlight.TakeOffLocation = updatedFlightRequest.TakeOffLocation;
                existingFlight.FlightDuration = updatedFlightRequest.FlightDuration;
                existingFlight.AirportLoc = updatedFlightRequest.AirportLoc;

                await _flightRepository.Update(existingFlight);
            }
        }

        public async Task DeleteFlightAsync(int flightId)
        {
            await _flightRepository.Delete(flightId);
        }
    }

}
