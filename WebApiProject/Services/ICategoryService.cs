using WebApiProject.DTOs;

namespace WebApiProject.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllWithTrainersAsync();
        Task<CategoryDto?> GetByIdAsync(int id);
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<IEnumerable<CategoryDto>> SearchAsync(string term);
        Task<CategoryDto> AddAsync(CategoryCreateDto dto);
        Task<CategoryDto?> UpdateAsync(int id, CategoryUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
