using FinalProject.Models;
namespace FinalProject.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetById(string id);
        Task Add(User entity);
        Task Update(User entity);
        Task Delete(string id);
        Task Save();
    }

}
