using CjsApi.Models;

namespace CjsApi.Repositories.McqRepository
{
    public interface IMcqRepository
    {
        Task<McqQuestion> CreateQuestionAsync(McqQuestion question);
        Task<List<McqQuestion>> GetAllQuestionsAsync(string? category = null);
    Task<List<string>> GetCategoriesAsync();
        Task<List<McqQuestion>> GetQuestionsByFiltersAsync(string? category, string? difficulty, int count);
        Task<McqQuestion?> GetQuestionByIdAsync(int id);
        Task<McqQuestion> UpdateQuestionAsync(McqQuestion question);
        Task<bool> DeleteQuestionAsync(int id);
        
        Task<QuizSession> CreateQuizSessionAsync(QuizSession session);
        Task<QuizSession?> GetQuizSessionAsync(int sessionId);
        Task<QuizSession> UpdateQuizSessionAsync(QuizSession session);
        
        Task<McqAttempt> CreateAttemptAsync(McqAttempt attempt);
        Task<List<McqAttempt>> GetAttemptsBySessionAsync(int sessionId);
        Task<List<QuizSession>> GetUserQuizHistoryAsync(int userId);
    }
}