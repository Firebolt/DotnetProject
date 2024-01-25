using FinalProject.Models;

namespace FinalProject.Services.Interfaces
{
    public interface IQueryService
    {
        Task<IEnumerable<Query>> GetAllQueriesAsync();
        Task<Query> GetQueryAsync(int queryId, int userId);
        Task CreateQueryAsync(QueryRequest queryRequest, int userId);
        Task UpdateQueryAsync(int queryId, int userId, QueryRequest updatedQueryRequest);
        Task DeleteQueryAsync(int queryId, int  userId);
    }
}
