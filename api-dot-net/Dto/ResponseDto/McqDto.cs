namespace CjsApi.Dto.ResponseDto
{
    public class McqQuestionDto
    {
        public int Id { get; set; }
        public string Question { get; set; } = null!;
        public string OptionA { get; set; } = null!;
        public string OptionB { get; set; } = null!;
        public string OptionC { get; set; } = null!;
        public string OptionD { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string Difficulty { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }

    public class McqQuestionWithAnswerDto : McqQuestionDto
    {
        public string CorrectOption { get; set; } = null!;
        public string CorrectExplanation { get; set; } = null!;
        public string? IncorrectExplanationA { get; set; }
        public string? IncorrectExplanationB { get; set; }
        public string? IncorrectExplanationC { get; set; }
        public string? IncorrectExplanationD { get; set; }
    }

    public class QuizSessionDto
    {
        public int Id { get; set; }
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public int IncorrectAnswers { get; set; }
        public double ScorePercentage { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public bool IsCompleted { get; set; }
    }

    public class McqAttemptResultDto
    {
        public bool IsCorrect { get; set; }
        public string CorrectOption { get; set; } = null!;
        public string Explanation { get; set; } = null!;
        public QuizSessionDto QuizSession { get; set; } = null!;
    }

    public class QuizResultDto
    {
        public QuizSessionDto Session { get; set; } = null!;
        public List<McqAttemptDetailDto> Attempts { get; set; } = new();
    }

    public class McqAttemptDetailDto
    {
        public int QuestionId { get; set; }
        public string Question { get; set; } = null!;
        public string SelectedOption { get; set; } = null!;
        public string CorrectOption { get; set; } = null!;
        public bool IsCorrect { get; set; }
        public string Explanation { get; set; } = null!;
    }
}