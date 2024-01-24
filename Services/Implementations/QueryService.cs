using FinalProject.Models;
using FinalProject.Repositories.Interfaces;
using FinalProject.Services.Interfaces;

namespace FinalProject.Services.Implementations
{
    public class QueryService : IQueryService
    {
        private readonly IRepository<Query> _queryRepository;

        public QueryService(IRepository<Query> queryRepository)
        {
            _queryRepository = queryRepository;
        }

        public async Task<IEnumerable<Query>> GetAllQueriesAsync()
        {
            return await _queryRepository.GetAllAsync();
        }

        public async Task<Query> GetQueryByIdAsync(int queryId)
        {
            return await _queryRepository.GetById(queryId);
        }

        public async Task CreateQueryAsync(QueryRequest queryRequest, int userId)
        {
            IRepository<User> user;
            var newQuery = new Query
            {
                UID = queryRequest.UID,
                Question = queryRequest.Question,
                Answer = queryRequest.Answer,
                User = user.GetById(userId)
            };

            await _queryRepository.Add(newQuery);
        }

        public async Task UpdateQueryAsync(int queryId, QueryRequest updatedQueryRequest)
        {
            var existingQuery = await _queryRepository.GetById(queryId);
            if (existingQuery != null)
            {
                existingQuery.Question = updatedQueryRequest.Question;
                existingQuery.Answer = updatedQueryRequest.Answer;

                await _queryRepository.Update(existingQuery);
            }
        }

        public async Task DeleteQueryAsync(int queryId)
        {
            await _queryRepository.Delete(queryId);
        }
    }
}
