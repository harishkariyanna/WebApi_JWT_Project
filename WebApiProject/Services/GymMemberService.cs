using WebApiProject.DTOs;
using WebApiProject.Models;
using WebApiProject.Repositories;

namespace WebApiProject.Services
{
    public class GymMemberService
    {
        private readonly IGymMemberRepository _gymMemberRepository;

        public GymMemberService(IGymMemberRepository gymMemberRepository)
        {
            _gymMemberRepository = gymMemberRepository;
        }

        public async Task<IEnumerable<GymMember>> GetAllWithCategoryAsync()
        {
            return await _gymMemberRepository.GetAllWithCategoryAsync();
        }

        public async Task<GymMember?> GetByIdWithCategoryAsync(int id)
        {
            return await _gymMemberRepository.GetByIdWithCategoryAsync(id);
        }

        // 🔍 Search by term
        public async Task<IEnumerable<GymMemberDto>> SearchAsync(string term)
        {
            var members = await _gymMemberRepository.SearchAsync(term);
            return members.Select(MapToDto);
        }

        // 📅 Exact join date
        public async Task<IEnumerable<GymMemberDto>> GetByJoinedDateAsync(DateTime date)
        {
            var members = await _gymMemberRepository.GetByJoinedDateAsync(date);
            return members.Select(MapToDto);
        }

        // 🔍+📅 Combined filter
        public async Task<IEnumerable<GymMemberDto>> SearchByTermAndDateRangeAsync(string term, DateTime? start, DateTime? end)
        {
            var members = await _gymMemberRepository.SearchByTermAndDateRangeAsync(term, start, end);
            return members.Select(MapToDto);
        }

        public async Task<GymMemberDto> CreateAsync(GymMemberCreateDto dto)
        {
            var member = new GymMember
            {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                Goals = dto.Goals,
                CategoryId = dto.CategoryId,
                TrainerId = dto.TrainerId,
                JoinedDate = DateTime.UtcNow
            };

            var created = await _gymMemberRepository.AddAsync(member);
            return MapToDto(created);
        }

        public async Task<GymMemberDto?> UpdateAsync(int id, GymMemberUpdateDto dto)
        {
            var member = await _gymMemberRepository.GetByIdAsync(id);
            if (member == null) return null;

            member.Name = dto.Name;
            member.Email = dto.Email;
            member.Phone = dto.Phone;
            member.Goals = dto.Goals;
            member.CategoryId = dto.CategoryId;
            member.TrainerId = dto.TrainerId;

            var updated = await _gymMemberRepository.UpdateAsync(member);
            return MapToDto(updated);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _gymMemberRepository.DeleteAsync(id);
        }

        private static GymMemberDto MapToDto(GymMember m) =>
            new GymMemberDto
            {
                Id = m.Id,
                Name = m.Name,
                Email = m.Email,
                Phone = m.Phone ?? string.Empty,
                Goals = m.Goals ?? string.Empty,
                CategoryId = m.CategoryId,
                TrainerId = m.TrainerId,
                JoinedDate = m.JoinedDate
            };
    }
}
