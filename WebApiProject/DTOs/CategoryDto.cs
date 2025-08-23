namespace WebApiProject.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public int CurrentMembers { get; set; }
        public int AvailableSlots { get; set; }
        public bool IsFull { get; set; }
    }
    public class CategoryCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Capacity { get; set; }
    }
    public class CategoryUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Capacity { get; set; }
    }

    public class CategoryWithTrainerDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int Capacity { get; set; }
        public int CurrentMembers { get; set; }
        public int AvailableSlots { get; set; }
        public bool IsFull { get; set; }
        public List<TrainerDto>? Trainers { get; set; }
    }
}
