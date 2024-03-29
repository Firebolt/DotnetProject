﻿using FinalProject.Models;
using FinalProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Repositories.Implementations
{
    public class QueryRepository : IQueryRepository
    {
        private readonly AppDbContext _context;

        public QueryRepository(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Query>> GetQueryAsync(string id)
        {
            return await _context.Queries
                .Where(q => q.Id == id).ToListAsync();
        }

        public async Task<Query> GetQueryAsync(int qid, string id)
        {
             return await _context.Queries
                .FirstOrDefaultAsync(q => q.QID == qid && q.Id == id);
        }

        public async Task<IEnumerable<Query>> GetAllQueriesAsync()
        {
            return await _context.Queries.ToListAsync();

        }
        public async Task AddQueryAsync(Query query)
        {
           _context.Queries.Add(query);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateQueryAsync(Query query)
        {
            _context.Queries.Update(query);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteQueryAsync(int qid, string uid)
        {
            var query= await GetQueryAsync(qid, uid);
            if(query!=null)
            {
                _context.Queries.Remove(query);
                await _context.SaveChangesAsync();
            }
        }
    }
}
