using CjsApi.Dto.RequestDto;
using CjsApi.Dto.ResponseDto;
using CjsApi.Models;
using CjsApi.Repositories.McqRepository;

namespace CjsApi.Services.McqService
{
    public class McqService : IMcqService
    {
        private readonly IMcqRepository _mcqRepository;

        public McqService(IMcqRepository mcqRepository)
        {
            _mcqRepository = mcqRepository;
        }

        public async Task<McqQuestionWithAnswerDto> CreateQuestionAsync(CreateMcqDto dto, int createdBy)
        {
            var question = new McqQuestion
            {
                Question = dto.Question,
                OptionA = dto.OptionA,
                OptionB = dto.OptionB,
                OptionC = dto.OptionC,
                OptionD = dto.OptionD,
                CorrectOption = dto.CorrectOption.ToUpper(),
                CorrectExplanation = dto.CorrectExplanation,
                IncorrectExplanationA = dto.IncorrectExplanationA,
                IncorrectExplanationB = dto.IncorrectExplanationB,
                IncorrectExplanationC = dto.IncorrectExplanationC,
                IncorrectExplanationD = dto.IncorrectExplanationD,
                Category = dto.Category,
                Difficulty = dto.Difficulty,
                CreatedBy = createdBy
            };

            var created = await _mcqRepository.CreateQuestionAsync(question);
            return MapToQuestionWithAnswerDto(created);
        }

        public async Task<List<McqQuestionWithAnswerDto>> GetAllQuestionsAsync(string? category = null)
        {
            var questions = await _mcqRepository.GetAllQuestionsAsync(category);
            return questions.Select(MapToQuestionWithAnswerDto).ToList();
        }

        public async Task<List<string>> GetCategoriesAsync()
        {
            return await _mcqRepository.GetCategoriesAsync();
        }

        public async Task<McqQuestionWithAnswerDto?> GetQuestionByIdAsync(int id)
        {
            var question = await _mcqRepository.GetQuestionByIdAsync(id);
            return question == null ? null : MapToQuestionWithAnswerDto(question);
        }

        public async Task<McqQuestionWithAnswerDto> UpdateQuestionAsync(int id, CreateMcqDto dto)
        {
            var question = await _mcqRepository.GetQuestionByIdAsync(id);
            if (question == null) throw new ArgumentException("Question not found");

            question.Question = dto.Question;
            question.OptionA = dto.OptionA;
            question.OptionB = dto.OptionB;
            question.OptionC = dto.OptionC;
            question.OptionD = dto.OptionD;
            question.CorrectOption = dto.CorrectOption.ToUpper();
            question.CorrectExplanation = dto.CorrectExplanation;
            question.IncorrectExplanationA = dto.IncorrectExplanationA;
            question.IncorrectExplanationB = dto.IncorrectExplanationB;
            question.IncorrectExplanationC = dto.IncorrectExplanationC;
            question.IncorrectExplanationD = dto.IncorrectExplanationD;
            question.Category = dto.Category;
            question.Difficulty = dto.Difficulty;

            var updated = await _mcqRepository.UpdateQuestionAsync(question);
            return MapToQuestionWithAnswerDto(updated);
        }

        public async Task<bool> DeleteQuestionAsync(int id)
        {
            return await _mcqRepository.DeleteQuestionAsync(id);
        }

        public async Task<QuizSessionDto> StartQuizAsync(StartQuizDto dto, int userId)
        {
            var session = new QuizSession
            {
                UserId = userId,
                TotalQuestions = dto.QuestionCount,
                CorrectAnswers = 0,
                IncorrectAnswers = 0,
                ScorePercentage = 0
            };

            var created = await _mcqRepository.CreateQuizSessionAsync(session);
            return MapToQuizSessionDto(created);
        }

        public async Task<McqAttemptResultDto> SubmitAnswerAsync(SubmitMcqAnswerDto dto, int userId)
        {
            var question = await _mcqRepository.GetQuestionByIdAsync(dto.QuestionId);
            if (question == null) throw new ArgumentException("Question not found");

            var session = await _mcqRepository.GetQuizSessionAsync(dto.QuizSessionId);
            if (session == null) throw new ArgumentException("Quiz session not found");

            var isCorrect = question.CorrectOption.Equals(dto.SelectedOption.ToUpper());
            
            var attempt = new McqAttempt
            {
                UserId = userId,
                QuestionId = dto.QuestionId,
                QuizSessionId = dto.QuizSessionId,
                SelectedOption = dto.SelectedOption.ToUpper(),
                IsCorrect = isCorrect
            };

            await _mcqRepository.CreateAttemptAsync(attempt);

            // Update session stats
            if (isCorrect)
                session.CorrectAnswers++;
            else
                session.IncorrectAnswers++;

            var totalAnswered = session.CorrectAnswers + session.IncorrectAnswers;
            session.ScorePercentage = (double)session.CorrectAnswers / totalAnswered * 100;

            if (totalAnswered >= session.TotalQuestions)
            {
                session.IsCompleted = true;
                session.CompletedAt = DateTime.UtcNow;
            }

            await _mcqRepository.UpdateQuizSessionAsync(session);

            var explanation = isCorrect ? question.CorrectExplanation : GetIncorrectExplanation(question, dto.SelectedOption);

            return new McqAttemptResultDto
            {
                IsCorrect = isCorrect,
                CorrectOption = question.CorrectOption,
                Explanation = explanation,
                QuizSession = MapToQuizSessionDto(session)
            };
        }

        public async Task<QuizResultDto> GetQuizResultAsync(int sessionId, int userId)
        {
            var session = await _mcqRepository.GetQuizSessionAsync(sessionId);
            if (session == null || session.UserId != userId) 
                throw new ArgumentException("Quiz session not found");

            var attempts = await _mcqRepository.GetAttemptsBySessionAsync(sessionId);

            return new QuizResultDto
            {
                Session = MapToQuizSessionDto(session),
                Attempts = attempts.Select(a => new McqAttemptDetailDto
                {
                    QuestionId = a.QuestionId,
                    Question = a.Question.Question,
                    SelectedOption = a.SelectedOption,
                    CorrectOption = a.Question.CorrectOption,
                    IsCorrect = a.IsCorrect,
                    Explanation = a.IsCorrect ? a.Question.CorrectExplanation : GetIncorrectExplanation(a.Question, a.SelectedOption)
                }).ToList()
            };
        }

        public async Task<List<QuizSessionDto>> GetUserQuizHistoryAsync(int userId)
        {
            var sessions = await _mcqRepository.GetUserQuizHistoryAsync(userId);
            return sessions.Select(MapToQuizSessionDto).ToList();
        }

        public async Task<List<McqQuestionDto>> GetQuizQuestionsAsync(int sessionId)
        {
            var session = await _mcqRepository.GetQuizSessionAsync(sessionId);
            if (session == null) throw new ArgumentException("Quiz session not found");

            var questions = await _mcqRepository.GetQuestionsByFiltersAsync(null, null, session.TotalQuestions);
            return questions.Select(MapToQuestionDto).ToList();
        }

        public async Task SaveAttemptAsync(SaveMcqAttemptDto dto, int userId)
        {
            var attempt = new McqAttempt
            {
                UserId = userId,
                QuestionId = dto.McqQuestionId,
                QuizSessionId = 0, // Default session for simple attempts
                SelectedOption = dto.SelectedOption.ToUpper(),
                IsCorrect = dto.IsCorrect,
                AttemptedAt = DateTime.UtcNow
            };

            await _mcqRepository.CreateAttemptAsync(attempt);
        }

        private static McqQuestionDto MapToQuestionDto(McqQuestion question)
        {
            return new McqQuestionDto
            {
                Id = question.Id,
                Question = question.Question,
                OptionA = question.OptionA,
                OptionB = question.OptionB,
                OptionC = question.OptionC,
                OptionD = question.OptionD,
                Category = question.Category,
                Difficulty = question.Difficulty,
                CreatedAt = question.CreatedAt
            };
        }

        private static McqQuestionWithAnswerDto MapToQuestionWithAnswerDto(McqQuestion question)
        {
            return new McqQuestionWithAnswerDto
            {
                Id = question.Id,
                Question = question.Question,
                OptionA = question.OptionA,
                OptionB = question.OptionB,
                OptionC = question.OptionC,
                OptionD = question.OptionD,
                CorrectOption = question.CorrectOption,
                CorrectExplanation = question.CorrectExplanation,
                IncorrectExplanationA = question.IncorrectExplanationA,
                IncorrectExplanationB = question.IncorrectExplanationB,
                IncorrectExplanationC = question.IncorrectExplanationC,
                IncorrectExplanationD = question.IncorrectExplanationD,
                Category = question.Category,
                Difficulty = question.Difficulty,
                CreatedAt = question.CreatedAt
            };
        }

        private static QuizSessionDto MapToQuizSessionDto(QuizSession session)
        {
            return new QuizSessionDto
            {
                Id = session.Id,
                TotalQuestions = session.TotalQuestions,
                CorrectAnswers = session.CorrectAnswers,
                IncorrectAnswers = session.IncorrectAnswers,
                ScorePercentage = session.ScorePercentage,
                StartedAt = session.StartedAt,
                CompletedAt = session.CompletedAt,
                IsCompleted = session.IsCompleted
            };
        }

        private static string GetIncorrectExplanation(McqQuestion question, string selectedOption)
        {
            return selectedOption.ToUpper() switch
            {
                "A" => question.IncorrectExplanationA ?? "This option is incorrect.",
                "B" => question.IncorrectExplanationB ?? "This option is incorrect.",
                "C" => question.IncorrectExplanationC ?? "This option is incorrect.",
                "D" => question.IncorrectExplanationD ?? "This option is incorrect.",
                _ => "This option is incorrect."
            };
        }
    }
}