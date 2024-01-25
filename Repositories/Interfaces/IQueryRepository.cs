using FinalProject.Models;

namespace FinalProject.Repositories.Interfaces
{
    public interface IQueryRepository
    {
        Task<Query> GetQueryAsync(int qid, int ui);
        Task<IEnumerable<Query>> GetAllQueriesAsync();
        Task AddQueryAsync(Query query);
        Task UpdateQueryAsync(Query query);
        Task DeleteQueryAsync(int qid, int uid);


    }
}
