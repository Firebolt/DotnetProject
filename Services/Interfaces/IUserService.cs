using FinalProject.Models;

namespace FinalProject.Services.Interfaces
{
    public interface IUserService
    {
            Task<IEnumerable<User>> GetAllUsersAsync();
            Task<User> GetUserByIdAsync(string userId);
            Task CreateUserAsync(UserRequest userRequest);
            Task UpdateUserAsync(string userId, UserRequest updatedUserRequest);
            Task DeleteUserAsync(string userId);
        }
    }

