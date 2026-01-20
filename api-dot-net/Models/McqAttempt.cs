using System.ComponentModel.DataAnnotations;

namespace CjsApi.Models
{
    public class McqAttempt
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public int QuizSessionId { get; set; }
        public int QuestionId { get; set; }
        public string SelectedOption { get; set; } = null!; // A, B, C, or D
        public bool IsCorrect { get; set; }
        public DateTime AttemptedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public User User { get; set; } = null!;
        public McqQuestion Question { get; set; } = null!;
        public QuizSession QuizSession { get; set; } = null!;
    }

    public class QuizSession
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public int IncorrectAnswers { get; set; }
        public double ScorePercentage { get; set; }
        public DateTime StartedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; }
        public bool IsCompleted { get; set; } = false;

        // Navigation
        public User User { get; set; } = null!;
        public ICollection<McqAttempt> Attempts { get; set; } = new List<McqAttempt>();
    }
}