using WebApiProject.Models;

namespace WebApiProject.Repositories
{
    public interface IGymMemberRepository
    {
        Task<GymMember?> GetByIdAsync(int id);
        Task<IEnumerable<GymMember>> GetAllAsync();
        Task<IEnumerable<GymMember>> SearchAsync(string term);
        Task<GymMember> AddAsync(GymMember entity);
        Task<GymMember> UpdateAsync(GymMember entity);
        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<GymMember>> GetMembersByTrainerIdAsync(int trainerId);
        Task<IEnumerable<GymMember>> GetAllWithCategoryAsync();
        Task<GymMember?> GetByIdWithCategoryAsync(int id);

        // ✅ New
        Task<IEnumerable<GymMember>> GetByJoinedDateAsync(DateTime date);
        Task<IEnumerable<GymMember>> GetByJoinedDateRangeAsync(DateTime start, DateTime end);
        Task<IEnumerable<GymMember>> SearchByTermAndDateRangeAsync(string term, DateTime? start, DateTime? end);
    }
}
