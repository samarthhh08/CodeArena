using CjsApi.Models;
using CjsApi.Repositories.ProblemRepository;

namespace CjsApi.Services.ProblemService
{
    public class ProblemService : IProblemService
    {
        private readonly IProblemRepository _problemRepository;

        public ProblemService(IProblemRepository problemRepository)
        {
            _problemRepository = problemRepository;
        }

        public async Task<IEnumerable<Problem>> GetProblemsAsync(
            Difficulty? difficulty,
            List<string>? tags
        )
        {
            return await _problemRepository.GetAllAsync(
                onlyPublished: true,
                difficulty: difficulty,
                tags: tags
            );
        }

        public async Task<Problem> GetProblemBySlugAsync(string slug)
        {
            var problem = await _problemRepository.GetBySlugAsync(slug);
            if (problem == null)
                throw new KeyNotFoundException("Problem not found");

            return problem;
        }


        public async Task<Problem> GetProblemByIdAsync(int id)
        {
            var problem = await _problemRepository.GetByIdAsync(id);
            if (problem == null)
                throw new KeyNotFoundException("Problem not found");

            return problem;
        }

        public async Task<Problem> CreateProblemAsync(Problem problem)
        {
            if (await _problemRepository.ExistsBySlugAsync(problem.Slug))
                throw new InvalidOperationException("Slug already exists");

            problem.CreatedAt = DateTime.UtcNow;
            problem.IsPublished = false; // admin publishes later

            return await _problemRepository.CreateAsync(problem);
        }

        public async Task UpdateProblemAsync(int id, Problem updatedProblem)
        {
            var existing = await _problemRepository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException("Problem not found");

            existing.Title = updatedProblem.Title;
            existing.Description = updatedProblem.Description;
            existing.Difficulty = updatedProblem.Difficulty;
            existing.TimeLimitMs = updatedProblem.TimeLimitMs;
            existing.MemoryLimitMb = updatedProblem.MemoryLimitMb;
            existing.IsPublished = updatedProblem.IsPublished;

            await _problemRepository.UpdateAsync(existing);
        }

        public async Task DeleteProblemAsync(int id)
        {
            await _problemRepository.DeleteAsync(id);
        }
    }
}
