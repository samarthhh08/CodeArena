using System.ComponentModel.DataAnnotations;

namespace CjsApi.Models
{
    public class Contest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public bool IsPublished { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public ICollection<ContestProblem> ContestProblems { get; set; }
            = new List<ContestProblem>();

        public ICollection<ContestParticipant> Participants { get; set; }
            = new List<ContestParticipant>();
    }
}
