using System.ComponentModel.DataAnnotations;

namespace CjsApi.Dto.RequestDto
{
    public class CreateMcqDto
    {
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
    }

    public class SubmitMcqAnswerDto
    {
        [Required]
        public int QuestionId { get; set; }

        [Required]
        public string SelectedOption { get; set; } = null!; // A, B, C, or D

        [Required]
        public int QuizSessionId { get; set; }
    }

    public class StartQuizDto
    {
        public int QuestionCount { get; set; } = 10;
        public string? Category { get; set; }
        public string? Difficulty { get; set; }
    }
}