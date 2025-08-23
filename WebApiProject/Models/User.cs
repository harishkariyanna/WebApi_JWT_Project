namespace WebApiProject.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = "User"; // e.g., Admin, Trainer, Member
        public string Token { get; set; } = string.Empty; // JWT token if needed
    }
}
