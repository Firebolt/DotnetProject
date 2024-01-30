using FinalProject.Models;

namespace FinalProject.Services.Interfaces
{
    public interface IQueryService
    {
        Task<IEnumerable<Query>> GetAllQueriesAsync();
        Task<Query> GetQueryAsync(int queryId, string userId);
        Task CreateQueryAsync(QueryRequest queryRequest, string userId);
        Task UpdateQueryAsync(int queryId, string userId, QueryRequest updatedQueryRequest);
        Task DeleteQueryAsync(int queryId, string  userId);
    }
}
