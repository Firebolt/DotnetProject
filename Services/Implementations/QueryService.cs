using FinalProject.Models;
using FinalProject.Repositories.Interfaces;
using FinalProject.Services.Interfaces;

namespace FinalProject.Services.Implementations
{
    public class QueryService : IQueryService
    {
        private readonly IQueryRepository _queryRepository;
        private readonly IRepository<User> _user;

        public QueryService(IQueryRepository queryRepository, IRepository<User> user)
        {
            _queryRepository = queryRepository;
            _user = user;
        }

        public async Task<IEnumerable<Query>> GetAllQueriesAsync()
        {
            return await _queryRepository.GetAllQueriesAsync();
        }

        public async Task<Query> GetQueryAsync(int queryId, int userId)
        {
            return await _queryRepository.GetQueryAsync(queryId, userId);
        }

        public async Task CreateQueryAsync(QueryRequest queryRequest, int userId)
        {
            var newQuery = new Query
            {
                UID = queryRequest.UID,
                Question = queryRequest.Question,
                Answer = queryRequest.Answer,
                User = await _user.GetById(userId)
            };

            await _queryRepository.AddQueryAsync(newQuery);
        }

        public async Task UpdateQueryAsync(int queryId, int userId, QueryRequest updatedQueryRequest)
        {
            var existingQuery = await _queryRepository.GetQueryAsync(queryId, userId);
            if (existingQuery != null)
            {
                existingQuery.Question = updatedQueryRequest.Question;
                existingQuery.Answer = updatedQueryRequest.Answer;

                await _queryRepository.UpdateQueryAsync(existingQuery);
            }
        }

        public async Task DeleteQueryAsync(int queryId, int userId)
        {
            await _queryRepository.DeleteQueryAsync(queryId, userId);
        }
    }
}
