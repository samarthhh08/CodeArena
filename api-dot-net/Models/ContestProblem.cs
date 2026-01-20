namespace CjsApi.Models
{
    public class ContestProblem
    {
        public int ContestId { get; set; }
        public Contest Contest { get; set; } = null!;

        public int ProblemId { get; set; }
        public Problem Problem { get; set; } = null!;

        public int Points { get; set; }

        public ICollection<ContestSubmission> Submissions { get; set; }
            = new List<ContestSubmission>();
    }

}
