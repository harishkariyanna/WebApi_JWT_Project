using WebApiProject.DTOs;
using WebApiProject.Models;
using WebApiProject.Repositories;

namespace WebApiProject.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // Get all categories (without trainer details)
        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories.Select(MapToDto);
        }

        // Get all categories WITH trainer details
        public async Task<IEnumerable<CategoryWithTrainerDto>> GetAllWithTrainersAsync()
        {
            var categories = await _categoryRepository.GetAllWithTrainersAsync();

            return categories.Select(c => new CategoryWithTrainerDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Capacity = c.Capacity,
                CurrentMembers = c.CurrentMembers,
                AvailableSlots = c.Capacity - c.CurrentMembers,
                IsFull = c.CurrentMembers >= c.Capacity,
                Trainers = c.Trainers?.Select(t => new TrainerDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Email = t.Email,
                    Experience = t.Experience
                }).ToList() ?? new List<TrainerDto>() // Ensure no null
            });
        }

        // Get single category (without trainer details)
        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            return category is null ? null : MapToDto(category);
        }

        // Get single category WITH trainers
        public async Task<CategoryWithTrainerDto?> GetByIdWithTrainersAsync(int id)
        {
            var category = await _categoryRepository.GetByIdWithTrainersAsync(id);
            if (category is null) return null;

            return new CategoryWithTrainerDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                Capacity = category.Capacity,
                CurrentMembers = category.CurrentMembers,
                AvailableSlots = category.Capacity - category.CurrentMembers,
                IsFull = category.CurrentMembers >= category.Capacity,
                Trainers = category.Trainers?.Select(t => new TrainerDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Email = t.Email,
                    Experience = t.Experience
                }).ToList() ?? new List<TrainerDto>() // Safe null handling
            };
        }

        // Create category
        public async Task<CategoryDto> CreateAsync(CategoryCreateDto dto)
        {
            var category = new Category
            {
                Name = dto.Name,
                Description = dto.Description,
                Capacity = dto.Capacity,
                CurrentMembers = 0 // Initialize to 0
            };

            var created = await _categoryRepository.AddAsync(category);
            return MapToDto(created);
        }

        // Update category
        public async Task<CategoryDto?> UpdateAsync(int id, CategoryUpdateDto dto)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category is null) return null;

            category.Name = dto.Name;
            category.Description = dto.Description;
            category.Capacity = dto.Capacity;

            var updated = await _categoryRepository.UpdateAsync(category);
            return MapToDto(updated);
        }

        // Delete category
        public async Task<bool> DeleteAsync(int id)
        {
            return await _categoryRepository.DeleteAsync(id);
        }

        // Helper method to map Category → CategoryDto
        private CategoryDto MapToDto(Category c)
        {
            return new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Capacity = c.Capacity,
                CurrentMembers = c.CurrentMembers,
                AvailableSlots = c.Capacity - c.CurrentMembers,
                IsFull = c.CurrentMembers >= c.Capacity
            };
        }
        public async Task<IEnumerable<CategoryDto>> SearchAsync(string term)
        {
            var categories = await _categoryRepository.SearchAsync(term);
            return categories.Select(MapToDto);
        }


    }
}
