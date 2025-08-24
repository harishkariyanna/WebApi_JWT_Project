using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiProject.DTOs;
using WebApiProject.Services;

[ApiController]
[Route("api/[controller]")]
public class TrainerController : ControllerBase
{
    private readonly TrainerService _trainerService;

    public TrainerController(TrainerService trainerService)
    {
        _trainerService = trainerService;
    }

    //Get all trainers with their category
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        var trainers = await _trainerService.GetAllWithCategoriesAsync();

        var result = trainers.Select(t => new
        {
            t.Id,
            t.Name,
            t.Email,
            t.Experience,
            t.Phone,
            t.Specialization,
            Category = t.Category == null ? null : new
            {
                t.Category.Id,
                t.Category.Name,
                t.Category.Description
            }
        });

        return Ok(result);
    }

    //Get single trainer by ID
    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById(int id)
    {
        var trainer = await _trainerService.GetByIdWithCategoriesAsync(id);
        if (trainer == null) return NotFound();

        var result = new
        {
            trainer.Id,
            trainer.Name,
            trainer.Email,
            trainer.Experience,
            trainer.Phone,
            trainer.Specialization,
            Category = trainer.Category == null ? null : new
            {
                trainer.Category.Id,
                trainer.Category.Name,
                trainer.Category.Description
            }
        };

        return Ok(result);
    }

    //Create new trainer (Admin only)
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(TrainerCreateDto dto)
    {
        var created = await _trainerService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    //Update trainer (Admin only)
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, TrainerUpdateDto dto)
    {
        var updated = await _trainerService.UpdateAsync(id, dto);
        if (updated == null) return NotFound();
        return Ok(updated);
    }

    //Delete trainer (Admin only)
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _trainerService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }

    // GET api/Trainer/search?term=Ram
    [HttpGet("search")]
    [Authorize]
    public async Task<IActionResult> Search([FromQuery] string term)
    {
        var trainers = await _trainerService.GetAllAsync();
        var filtered = trainers.Where(t =>
            t.Name.Contains(term, StringComparison.OrdinalIgnoreCase) ||
            t.Email.Contains(term, StringComparison.OrdinalIgnoreCase) ||
            t.Specialization.Contains(term, StringComparison.OrdinalIgnoreCase));
        return Ok(filtered);
    }


}
