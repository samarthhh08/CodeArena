using CjsApi.Models;

namespace CjsApi.Services.ProblemService
{
    public interface IProblemService
    {

        public IQueryable<Problem> GetProblemsQueryable(
         Difficulty? difficulty,
         List<string>? tags
     );
        Task<IEnumerable<Problem>> GetProblemsAsync(
            Difficulty? difficulty,
            List<string>? tags
        );

        Task<Problem> GetProblemBySlugAsync(string slug);
        Task<Problem> GetProblemByIdAsync(int id);

        Task<Problem> CreateProblemAsync(Problem problem);
        Task UpdateProblemAsync(int id, Problem updatedProblem);
        Task DeleteProblemAsync(int id);
    }
}
