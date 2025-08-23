using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiProject.DTOs;
using WebApiProject.Services;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly CategoryService _categoryService;

    public CategoryController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    // ✅ Get all categories with trainer details
    [HttpGet]
    [Authorize] // Only requires a valid token
    public async Task<IActionResult> GetAll()
    {
        var categories = await _categoryService.GetAllWithTrainersAsync(); // Updated service call

        var result = categories.Select(c => new
        {
            c.Id,
            c.Name,
            c.Description,
            Trainers = c.Trainers?.Select(t => new
            {
                t.Id,
                t.Name,
                t.Email,
                t.Experience
            })
        });

        return Ok(result);
    }

    // ✅ Get single category with trainer details
    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById(int id)
    {
        var category = await _categoryService.GetByIdWithTrainersAsync(id); // Updated method name
        if (category == null) return NotFound();

        var result = new
        {
            category.Id,
            category.Name,
            category.Description,
            Trainers = category.Trainers?.Select(t => new
            {
                t.Id,
                t.Name,
                t.Email,
                t.Experience
            })
        };

        return Ok(result);
    }

    // ✅ Admin-only operations
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(CategoryCreateDto dto)
    {
        var created = await _categoryService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, CategoryUpdateDto dto)
    {
        var updated = await _categoryService.UpdateAsync(id, dto);
        if (updated == null) return NotFound();
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _categoryService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }

    [HttpGet("search")]
    [Authorize]
    public async Task<IActionResult> Search([FromQuery] string term)
    {
        var results = await _categoryService.SearchAsync(term);
        return Ok(results);
    }

}
