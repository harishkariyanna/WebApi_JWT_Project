using Microsoft.EntityFrameworkCore;
using WebApiProject.Data;
using WebApiProject.Models;

namespace WebApiProject.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get single category by ID (without trainers)
        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        // Get all categories (without trainers)
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        // Get all categories with trainers included
        public async Task<IEnumerable<Category>> GetAllWithTrainersAsync()
        {
            return await _context.Categories
                .Include(c => c.Trainers) // Eager load trainers
                .ToListAsync();
        }

        // Get single category by ID with trainers included
        public async Task<Category?> GetByIdWithTrainersAsync(int id)
        {
            return await _context.Categories
                .Include(c => c.Trainers)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        // Search categories by name
        public async Task<IEnumerable<Category>> SearchAsync(string term)
        {
            return await _context.Categories
                .Where(c => c.Name.Contains(term))
                .ToListAsync();
        }

        // Add a new category
        public async Task<Category> AddAsync(Category entity)
        {
            _context.Categories.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        // Update an existing category
        public async Task<Category> UpdateAsync(Category entity)
        {
            _context.Categories.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        // Delete a category
        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
