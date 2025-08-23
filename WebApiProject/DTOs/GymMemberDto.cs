namespace WebApiProject.DTOs
{
    public class GymMemberCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Goals { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public int TrainerId { get; set; }
    }

    public class GymMemberUpdateDto : GymMemberCreateDto
    {
    }

    public class GymMemberDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Goals { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public int TrainerId { get; set; }
        public DateTime JoinedDate { get; set; }
    }
}
