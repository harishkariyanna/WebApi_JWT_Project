public class GymMember
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
    public string? Goals { get; set; }

    // New: when the member joined
    public DateTime JoinedDate { get; set; } = DateTime.UtcNow;

    public int TrainerId { get; set; }
    public Trainer? Trainer { get; set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
}
