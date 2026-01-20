using CjsApi.Dto.RequestDto;
using CjsApi.Dto.ResponseDto;

namespace CjsApi.Services.McqService
{
    public interface IMcqService
    {
        Task<McqQuestionWithAnswerDto> CreateQuestionAsync(CreateMcqDto dto, int createdBy);
        Task<List<McqQuestionWithAnswerDto>> GetAllQuestionsAsync(string? category = null);
        Task<List<string>> GetCategoriesAsync();
        Task<McqQuestionWithAnswerDto?> GetQuestionByIdAsync(int id);
        Task<McqQuestionWithAnswerDto> UpdateQuestionAsync(int id, CreateMcqDto dto);
        Task<bool> DeleteQuestionAsync(int id);
        
        Task<QuizSessionDto> StartQuizAsync(StartQuizDto dto, int userId);
        Task<McqAttemptResultDto> SubmitAnswerAsync(SubmitMcqAnswerDto dto, int userId);
        Task<QuizResultDto> GetQuizResultAsync(int sessionId, int userId);
        Task<List<QuizSessionDto>> GetUserQuizHistoryAsync(int userId);
        Task<List<McqQuestionDto>> GetQuizQuestionsAsync(int sessionId);
        Task SaveAttemptAsync(SaveMcqAttemptDto dto, int userId);
    }
}