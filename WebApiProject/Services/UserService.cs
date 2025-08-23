using WebApiProject.DTOs;
using WebApiProject.Models;
using WebApiProject.Repositories;

namespace WebApiProject.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _userRepo.GetAllAsync();
            return users.Select(u => new UserDto
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                Role = u.Role,
                Password = u.Password
            });
        }

        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null) return null;

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role
            };
        }

        public async Task<UserDto> AddAsync(UserCreateDto dto)
        {
            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                Password = dto.Password, // Ideally hash this before saving
                Role = dto.Role
            };

            var created = await _userRepo.AddAsync(user);

            return new UserDto
            {
                Id = created.Id,
                Username = created.Username,
                Email = created.Email,
                Role = created.Role
            };
        }

        public async Task<UserDto?> UpdateAsync(int id, UserUpdateDto dto)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null) return null;

            user.Email = dto.Email;
            user.Role = dto.Role;

            var updated = await _userRepo.UpdateAsync(user);

            return new UserDto
            {
                Id = updated.Id,
                Username = updated.Username,
                Email = updated.Email,
                Role = updated.Role
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _userRepo.DeleteAsync(id);
        }
    }
}
