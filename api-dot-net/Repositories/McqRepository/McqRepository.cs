using CjsApi.Data;
using CjsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CjsApi.Repositories.McqRepository
{
    public class McqRepository : IMcqRepository
    {
        private readonly AppDbContext _context;

        public McqRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<McqQuestion> CreateQuestionAsync(McqQuestion question)
        {
            _context.McqQuestions.Add(question);
            await _context.SaveChangesAsync();
            return question;
        }

        public async Task<List<McqQuestion>> GetAllQuestionsAsync(string? category = null)
        {
            var query = _context.McqQuestions
                .Include(q => q.Creator)
                .AsQueryable();

            if (!string.IsNullOrEmpty(category))
                query = query.Where(q => q.Category == category);

            return await query
                .OrderByDescending(q => q.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<string>> GetCategoriesAsync()
        {
            return await _context.McqQuestions
                .Select(q => q.Category)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();
        }

        public async Task<List<McqQuestion>> GetQuestionsByFiltersAsync(string? category, string? difficulty, int count)
        {
            var query = _context.McqQuestions.AsQueryable();

            if (!string.IsNullOrEmpty(category))
                query = query.Where(q => q.Category == category);

            if (!string.IsNullOrEmpty(difficulty))
                query = query.Where(q => q.Difficulty == difficulty);

            return await query
                .OrderBy(q => Guid.NewGuid()) // Random order
                .Take(count)
                .ToListAsync();
        }

        public async Task<McqQuestion?> GetQuestionByIdAsync(int id)
        {
            return await _context.McqQuestions
                .Include(q => q.Creator)
                .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<McqQuestion> UpdateQuestionAsync(McqQuestion question)
        {
            _context.McqQuestions.Update(question);
            await _context.SaveChangesAsync();
            return question;
        }

        public async Task<bool> DeleteQuestionAsync(int id)
        {
            var question = await _context.McqQuestions.FindAsync(id);
            if (question == null) return false;

            _context.McqQuestions.Remove(question);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<QuizSession> CreateQuizSessionAsync(QuizSession session)
        {
            _context.QuizSessions.Add(session);
            await _context.SaveChangesAsync();
            return session;
        }

        public async Task<QuizSession?> GetQuizSessionAsync(int sessionId)
        {
            return await _context.QuizSessions
                .Include(s => s.Attempts)
                .ThenInclude(a => a.Question)
                .FirstOrDefaultAsync(s => s.Id == sessionId);
        }

        public async Task<QuizSession> UpdateQuizSessionAsync(QuizSession session)
        {
            _context.QuizSessions.Update(session);
            await _context.SaveChangesAsync();
            return session;
        }

        public async Task<McqAttempt> CreateAttemptAsync(McqAttempt attempt)
        {
            _context.McqAttempts.Add(attempt);
            await _context.SaveChangesAsync();
            return attempt;
        }

        public async Task<List<McqAttempt>> GetAttemptsBySessionAsync(int sessionId)
        {
            return await _context.McqAttempts
                .Include(a => a.Question)
                .Where(a => a.QuizSessionId == sessionId)
                .ToListAsync();
        }

        public async Task<List<QuizSession>> GetUserQuizHistoryAsync(int userId)
        {
            return await _context.QuizSessions
                .Where(s => s.UserId == userId)
                .OrderByDescending(s => s.StartedAt)
                .ToListAsync();
        }
    }
}