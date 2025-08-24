using WebApiProject.DTOs;
using WebApiProject.Models;
using WebApiProject.Repositories;

namespace WebApiProject.Services
{
    public class TrainerService
    {
        private readonly ITrainerRepository _trainerRepository;
        
        public TrainerService(ITrainerRepository trainerRepository)
        {
            _trainerRepository = trainerRepository;
        }

        // Regular GetAll
        public async Task<IEnumerable<TrainerDto>> GetAllAsync()
        {
            var trainers = await _trainerRepository.GetAllAsync();
            return trainers.Select(t => new TrainerDto
            {
                Id = t.Id,
                Name = t.Name,
                Email = t.Email,
                Experience = t.Experience,
                CategoryId = t.CategoryId
            });
        }

        // Include related GymMembers & Categories
        public async Task<IEnumerable<Trainer>> GetAllWithCategoriesAsync()
        {
            return await _trainerRepository.GetAllWithCategoriesAsync();
        }

        public async Task<Trainer?> GetByIdWithCategoriesAsync(int id)
        {
            return await _trainerRepository.GetByIdWithCategoriesAsync(id);
        }

        // CRUD operations
        public async Task<TrainerDto> CreateAsync(TrainerCreateDto dto)
        {
            var trainer = new Trainer
            {
                Name = dto.Name,
                Email = dto.Email,
                Experience = dto.Experience,
                CategoryId = dto.CategoryId
            };
            var created = await _trainerRepository.AddAsync(trainer);
            return new TrainerDto
            {
                Id = created.Id,
                Name = created.Name,
                Email = created.Email,
                Experience = created.Experience,
                CategoryId = created.CategoryId
            };
        }

        public async Task<TrainerDto?> UpdateAsync(int id, TrainerUpdateDto dto)
        {
            var trainer = await _trainerRepository.GetByIdAsync(id);
            if (trainer == null) return null;

            trainer.Name = dto.Name;
            trainer.Email = dto.Email;
            trainer.Experience = dto.Experience;
            trainer.CategoryId = dto.CategoryId;

            var updated = await _trainerRepository.UpdateAsync(trainer);

            return new TrainerDto
            {
                Id = updated.Id,
                Name = updated.Name,
                Email = updated.Email,
                Experience = updated.Experience,
                CategoryId = updated.CategoryId
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _trainerRepository.DeleteAsync(id);
        }
    }
}
