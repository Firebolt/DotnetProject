using FinalProject.Models;
using FinalProject.Repositories.Interfaces;
using FinalProject.Services.Interfaces;

namespace FinalProject.Services.Implementations
{
    public class QueryService : IQueryService
    {
        private readonly IQueryRepository _queryRepository;
        private readonly IUserRepository _user;

        public QueryService(IQueryRepository queryRepository, IUserRepository user)
        {
            _queryRepository = queryRepository;
            _user = user;
        }

        public async Task<IEnumerable<Query>> GetAllQueriesAsync()
        {
            return await _queryRepository.GetAllQueriesAsync();
        }

        public async Task<Query> GetQueryAsync(int queryId, string userId)
        {
            return await _queryRepository.GetQueryAsync(queryId, userId);
        }

        public async Task CreateQueryAsync(QueryRequest queryRequest, string userId)
        {
            var newQuery = new Query
            {
                Question = queryRequest.Question,
                Answer = queryRequest.Answer,
                Id = userId,
                User = await _user.GetById(userId)
            };

            await _queryRepository.AddQueryAsync(newQuery);
        }

        public async Task UpdateQueryAsync(int queryId, string userId, QueryRequest updatedQueryRequest)
        {
            var existingQuery = await _queryRepository.GetQueryAsync(queryId, userId);
            if (existingQuery != null)
            {
                existingQuery.Question = updatedQueryRequest.Question;
                existingQuery.Answer = updatedQueryRequest.Answer;

                await _queryRepository.UpdateQueryAsync(existingQuery);
            }
        }

        public async Task DeleteQueryAsync(int queryId, string userId)
        {
            await _queryRepository.DeleteQueryAsync(queryId, userId);
        }
    }
}
