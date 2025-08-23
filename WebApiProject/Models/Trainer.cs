using WebApiProject.Models;

public class Trainer
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
    public string? Specialization { get; set; }
    public int Experience { get; set; }

    // Foreign key
    public int CategoryId { get; set; }

    // Navigation property
    public Category? Category { get; set; }
    public ICollection<GymMember>? GymMembers { get; set; }
}