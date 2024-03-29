﻿using FinalProject.Models;

namespace FinalProject.Repositories.Interfaces
{
    public interface ITicketRepository
    {
        Task<Ticket> GetTicketAsync(string uid, int fid);
        Task<IEnumerable<Ticket>> GetTicketsAsync(string uid);
        Task<IEnumerable<Ticket>> GetAllTicketAsync();
        Task AddTicketAsync(Ticket ticket);
        Task UpdateTicketAsync(Ticket ticket);
        Task DeleteTicketAsync(string uid, int fid);
    
    }
}
