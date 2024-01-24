using FinalProject.Models;

namespace FinalProject.Services.Interfaces
{
    public interface IQueryService
    {
        Task<IEnumerable<Query>> GetAllQueriesAsync();
        Task<Query> GetQueryByIdAsync(int queryId);
        Task CreateQueryAsync(QueryRequest queryRequest);
        Task UpdateQueryAsync(int queryId, QueryRequest updatedQueryRequest);
        Task DeleteQueryAsync(int queryId);
    }
}
