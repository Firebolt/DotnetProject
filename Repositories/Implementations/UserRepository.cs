using FinalProject.Models;
using FinalProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace FinalProject.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Set<User>().ToListAsync();
        }

        public async Task Add(User entity)
        {
            _context.Set<User>().Add(entity);
            await Save();
        }

        public async Task Delete(string id)
        {
            var entity = await _context.Set<User>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<User>().Remove(entity);
                await Save();
            }
        }

        public async Task<User> GetById(string id)
        {
            return await _context.Set<User>().FindAsync(id);
        }

        public async Task Update(User entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await Save();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}

