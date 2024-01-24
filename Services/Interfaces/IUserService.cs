using FinalProject.Models;

namespace FinalProject.Services.Interfaces
{
    public interface IUserService
    {
            Task<IEnumerable<User>> GetAllUsersAsync();
            Task<User> GetUserByIdAsync(int userId);
            Task CreateUserAsync(UserRequest userRequest);
            Task UpdateUserAsync(int userId, UserRequest updatedUserRequest);
            Task DeleteUserAsync(int userId);
        }
    }

