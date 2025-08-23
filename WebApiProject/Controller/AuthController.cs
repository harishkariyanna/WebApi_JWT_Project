using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiProject.Data;
using WebApiProject.Models;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _config;

    public AuthController(ApplicationDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto model)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username && u.Password == model.Password);
        if (user == null)
            return Unauthorized("Invalid credentials");

        var token = GenerateJwtToken(user);
        return Ok(new { token });
    }

    private string GenerateJwtToken(User user)
    {
        var key = _config["Jwt:Key"]
                  ?? throw new InvalidOperationException("JWT Key is missing in configuration.");
        var issuer = _config["Jwt:Issuer"]
                     ?? throw new InvalidOperationException("JWT Issuer is missing in configuration.");

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.Role, user.Role)
    };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: issuer,
            claims: claims,
            expires: DateTime.Now.AddDays(2),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}

public class LoginDto
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
