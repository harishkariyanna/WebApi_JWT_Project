namespace WebApiProject.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        // Include password for testing only
        public string Password { get; set; } = string.Empty;
    }
    public class UserCreateDto
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = "Member"; // default role
    }
    public class UserUpdateDto
    {
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = "Member";
    }
}
