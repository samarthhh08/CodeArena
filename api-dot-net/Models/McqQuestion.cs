using System.ComponentModel.DataAnnotations;

namespace CjsApi.Models
{
    public class McqQuestion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Question { get; set; } = null!;

        [Required]
        public string OptionA { get; set; } = null!;

        [Required]
        public string OptionB { get; set; } = null!;

        [Required]
        public string OptionC { get; set; } = null!;

        [Required]
        public string OptionD { get; set; } = null!;

        [Required]
        public string CorrectOption { get; set; } = null!; // A, B, C, or D

        [Required]
        public string CorrectExplanation { get; set; } = null!;

        public string? IncorrectExplanationA { get; set; }
        public string? IncorrectExplanationB { get; set; }
        public string? IncorrectExplanationC { get; set; }
        public string? IncorrectExplanationD { get; set; }

        public string Category { get; set; } = "General";
        public string Difficulty { get; set; } = "Medium";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int CreatedBy { get; set; }

        // Navigation
        public User Creator { get; set; } = null!;
        public ICollection<McqAttempt> Attempts { get; set; } = new List<McqAttempt>();
    }
}