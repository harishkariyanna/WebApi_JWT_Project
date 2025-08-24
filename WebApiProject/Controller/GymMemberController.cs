using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiProject.DTOs;              //Make sure this is present
using WebApiProject.Services;
using System;                          //Needed for DateTime

[ApiController]
[Route("api/[controller]")]
public class GymMemberController : ControllerBase
{
    private readonly GymMemberService _gymMemberService;

    public GymMemberController(GymMemberService gymMemberService)
    {
        _gymMemberService = gymMemberService;
    }

    // Get all gym members with category details
    // GET all members
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        var members = await _gymMemberService.GetAllWithCategoryAsync();

        var result = members.Select(m => new
        {
            m.Id,
            m.Name,
            m.Email,
            m.Phone,
            m.Goals,
            m.JoinedDate, // ✅ Include date
            Category = m.Category == null ? null : new
            {
                m.Category.Id,
                m.Category.Name,
                m.Category.Description
            }
        });

        return Ok(result);
    }


    //Get single gym member
    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById(int id)
    {
        var member = await _gymMemberService.GetByIdWithCategoryAsync(id);
        if (member == null) return NotFound();

        var result = new
        {
            member.Id,
            member.Name,
            member.Email,
            member.Phone,
            member.Goals,
            member.JoinedDate, // ✅ Include date
            Category = member.Category == null ? null : new
            {
                member.Category.Id,
                member.Category.Name,
                member.Category.Description
            }
        };


        return Ok(result);
    }

    //Search by term (Name, Email, Goals)
    [HttpGet("search")]
    [Authorize]
    public async Task<IActionResult> Search([FromQuery] string term)
    {
        var results = await _gymMemberService.SearchAsync(term);
        return Ok(results);
    }

    //Filter by exact join date
    [HttpGet("joined-on")]
    [Authorize]
    public async Task<IActionResult> GetByJoinedDate([FromQuery] DateTime date)
    {
        var results = await _gymMemberService.GetByJoinedDateAsync(date);
        return Ok(results);
    }

    //Filter by date range + optional term
    [HttpGet("filter")]
    [Authorize]
    public async Task<IActionResult> Filter([FromQuery] string? term, [FromQuery] DateTime? start, [FromQuery] DateTime? end)
    {
        if (start.HasValue && end.HasValue && end < start)
            return BadRequest("End date must be on or after start date.");

        var results = await _gymMemberService.SearchByTermAndDateRangeAsync(term ?? string.Empty, start, end);
        return Ok(results);
    }

    //Admin operations
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] GymMemberCreateDto dto)
    {
        var created = await _gymMemberService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] GymMemberUpdateDto dto)
    {
        var updated = await _gymMemberService.UpdateAsync(id, dto);
        if (updated == null) return NotFound();
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _gymMemberService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
