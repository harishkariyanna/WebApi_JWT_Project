using Microsoft.EntityFrameworkCore;
using WebApiProject.Data;
using WebApiProject.Models;

namespace WebApiProject.Repositories
{
    public class TrainerRepository : ITrainerRepository
    {
        private readonly ApplicationDbContext _context;

        public TrainerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Trainer?> GetByIdAsync(int id)
        {
            return await _context.Trainers.FindAsync(id);
        }

        public async Task<IEnumerable<Trainer>> GetAllAsync()
        {
            return await _context.Trainers.ToListAsync();
        }

        public async Task<IEnumerable<Trainer>> SearchAsync(string term)
        {
            return await _context.Trainers
                .Where(t => t.Name.Contains(term))
                .ToListAsync();
        }

        public async Task<Trainer> AddAsync(Trainer entity)
        {
            _context.Trainers.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Trainer> UpdateAsync(Trainer entity)
        {
            _context.Trainers.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var trainer = await _context.Trainers.FindAsync(id);
            if (trainer == null) return false;

            _context.Trainers.Remove(trainer);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Trainer>> GetTrainersByCategoryIdAsync(int categoryId)
        {
            return await _context.Trainers
                .Where(t => t.CategoryId == categoryId)
                .ToListAsync();
        }

        // Include Category (single) for each Trainer
        public async Task<IEnumerable<Trainer>> GetAllWithCategoriesAsync()
        {
            return await _context.Trainers
                .Include(t => t.Category)
                .ToListAsync();
        }

        public async Task<Trainer?> GetByIdWithCategoriesAsync(int id)
        {
            return await _context.Trainers
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
