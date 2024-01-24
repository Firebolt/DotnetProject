using FinalProject.Models;
using FinalProject.Repositories.Interfaces;
using FinalProject.Services.Interfaces;

namespace FinalProject.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _userRepository.GetById(userId);
        }

        public async Task CreateUserAsync(UserRequest userRequest)
        {
            var newUser = new User
            {
                Username = userRequest.Username,
                Password = userRequest.Password,
                UserRole = userRequest.UserRole,
                Email = userRequest.Email
            };

            await _userRepository.Add(newUser);
        }

        public async Task UpdateUserAsync(int userId, UserRequest updatedUserRequest)
        {
            var existingUser = await _userRepository.GetById(userId);
            if (existingUser != null)
            {
                existingUser.Username = updatedUserRequest.Username;
                existingUser.Password = updatedUserRequest.Password;
                existingUser.UserRole = updatedUserRequest.UserRole;

                await _userRepository.Update(existingUser);
            }
        }

        public async Task DeleteUserAsync(int userId)
        {
            await _userRepository.Delete(userId);
        }
    }
}
