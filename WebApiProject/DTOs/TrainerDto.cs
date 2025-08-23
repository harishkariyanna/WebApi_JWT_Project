namespace WebApiProject.DTOs
{
    public class TrainerDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public int Experience { get; set; }
        public int CategoryId { get; set; }
    }
    public class TrainerCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public int Experience { get; set; }
        public int CategoryId { get; set; }
    }
    public class TrainerUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public int Experience { get; set; }
        public int CategoryId { get; set; }
    }
}
