using Microsoft.EntityFrameworkCore;
using WebApiProject.Data;
using WebApiProject.Models;

namespace WebApiProject.Repositories
{
    public class GymMemberRepository : IGymMemberRepository
    {
        private readonly ApplicationDbContext _context;

        public GymMemberRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GymMember?> GetByIdAsync(int id)
        {
            return await _context.GymMembers.FindAsync(id);
        }

        public async Task<IEnumerable<GymMember>> GetAllAsync()
        {
            return await _context.GymMembers.ToListAsync();
        }

        public async Task<IEnumerable<GymMember>> SearchAsync(string term)
        {
            return await _context.GymMembers
                .Where(m => m.Name.Contains(term) ||
                            m.Email.Contains(term) ||
                            (m.Goals != null && m.Goals.Contains(term)))
                .ToListAsync();
        }

        public async Task<GymMember> AddAsync(GymMember entity)
        {
            _context.GymMembers.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<GymMember> UpdateAsync(GymMember entity)
        {
            _context.GymMembers.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var member = await _context.GymMembers.FindAsync(id);
            if (member == null) return false;

            _context.GymMembers.Remove(member);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<GymMember>> GetMembersByTrainerIdAsync(int trainerId)
        {
            return await _context.GymMembers
                .Where(m => m.TrainerId == trainerId)
                .ToListAsync();
        }

        // ✅ Include Category and Trainer
        public async Task<IEnumerable<GymMember>> GetAllWithCategoryAsync()
        {
            return await _context.GymMembers
                .Include(m => m.Category)
                .Include(m => m.Trainer)
                .ToListAsync();
        }

        public async Task<GymMember?> GetByIdWithCategoryAsync(int id)
        {
            return await _context.GymMembers
                .Include(m => m.Category)
                .Include(m => m.Trainer)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        // 📅 New: Get members by a specific join date
        public async Task<IEnumerable<GymMember>> GetByJoinedDateAsync(DateTime date)
        {
            var start = date.Date;
            var end = start.AddDays(1);
            return await _context.GymMembers
                .Where(m => m.JoinedDate >= start && m.JoinedDate < end)
                .ToListAsync();
        }

        // 📅 New: Get members in a date range
        public async Task<IEnumerable<GymMember>> GetByJoinedDateRangeAsync(DateTime start, DateTime end)
        {
            var startDate = start.Date;
            var endDateExclusive = end.Date.AddDays(1);
            return await _context.GymMembers
                .Where(m => m.JoinedDate >= startDate && m.JoinedDate < endDateExclusive)
                .ToListAsync();
        }

        // 🔍+📅 New: Search with optional term + optional date range
        public async Task<IEnumerable<GymMember>> SearchByTermAndDateRangeAsync(string term, DateTime? start, DateTime? end)
        {
            var query = _context.GymMembers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(term))
            {
                query = query.Where(m =>
                    m.Name.Contains(term) ||
                    m.Email.Contains(term) ||
                    (m.Goals != null && m.Goals.Contains(term)));
            }

            if (start.HasValue)
            {
                var s = start.Value.Date;
                query = query.Where(m => m.JoinedDate >= s);
            }

            if (end.HasValue)
            {
                var e = end.Value.Date.AddDays(1);
                query = query.Where(m => m.JoinedDate < e);
            }

            return await query.ToListAsync();
        }
    }
}
