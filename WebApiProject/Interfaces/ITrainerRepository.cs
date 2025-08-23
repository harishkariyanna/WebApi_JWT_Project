using WebApiProject.Models;

namespace WebApiProject.Repositories
{
    public interface ITrainerRepository
    {
        Task<Trainer?> GetByIdAsync(int id);
        Task<IEnumerable<Trainer>> GetAllAsync();
        Task<IEnumerable<Trainer>> SearchAsync(string term);
        Task<Trainer> AddAsync(Trainer entity);
        Task<Trainer> UpdateAsync(Trainer entity);
        Task<bool> DeleteAsync(int id);

        // Extra methods
        Task<IEnumerable<Trainer>> GetTrainersByCategoryIdAsync(int categoryId);

        // ✅ New methods for fetching related Categories
        Task<IEnumerable<Trainer>> GetAllWithCategoriesAsync();
        Task<Trainer?> GetByIdWithCategoriesAsync(int id);
    }
}
