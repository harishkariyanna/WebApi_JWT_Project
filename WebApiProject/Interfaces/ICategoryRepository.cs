using WebApiProject.Models;

namespace WebApiProject.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task<IEnumerable<Category>> SearchAsync(string term);
        Task<Category> AddAsync(Category entity);
        Task<Category> UpdateAsync(Category entity);
        Task<bool> DeleteAsync(int id);

        // ✅ Add these for trainers
        Task<IEnumerable<Category>> GetAllWithTrainersAsync();
        Task<Category?> GetByIdWithTrainersAsync(int id);
    }
}
