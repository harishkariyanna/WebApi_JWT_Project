using WebApiProject.Models;

namespace WebApiProject.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByEmailAsync(string email); // ✅ Add this
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> AddAsync(User entity);
        Task<User> UpdateAsync(User entity);
        Task<bool> DeleteAsync(int id);
    }
}
