using WebApiProject.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int Capacity { get; set; }

    // Track how many members are enrolled
    public int CurrentMembers { get; set; }

    // Navigation property
    public ICollection<Trainer>? Trainers { get; set; }
    public ICollection<GymMember>? GymMembers { get; set; }
}