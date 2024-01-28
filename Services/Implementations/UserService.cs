using FinalProject.Models;
using FinalProject.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _userManager.FindByIdAsync(userId.ToString());
        }

        public async Task CreateUserAsync(UserRequest userRequest)
        {
            var newUser = new User
            {
                UserName = userRequest.Username,
                UserRole = userRequest.UserRole,
                Email = userRequest.Email
            };

            await _userManager.CreateAsync(newUser, userRequest.Password);
        }

        public async Task UpdateUserAsync(int userId, UserRequest updatedUserRequest)
        {
            var existingUser = await _userManager.FindByIdAsync(userId.ToString());
            if (existingUser != null)
            {
                existingUser.UserName = updatedUserRequest.Username;
                existingUser.Email = updatedUserRequest.Email;
                existingUser.UserRole = updatedUserRequest.UserRole;

                if (!string.IsNullOrEmpty(updatedUserRequest.Password))
                {
                    var newPasswordHash = _userManager.PasswordHasher.HashPassword(existingUser, updatedUserRequest.Password);
                    existingUser.PasswordHash = newPasswordHash;
                }

                await _userManager.UpdateAsync(existingUser);
            }
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }
    }
}
