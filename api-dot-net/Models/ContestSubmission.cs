using System.ComponentModel.DataAnnotations;

namespace CjsApi.Models
{
    public class ContestSubmission
    {
        [Key]
        public int Id { get; set; }

        public int ContestId { get; set; }
        public int ProblemId { get; set; }

        public ContestProblem ContestProblem { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public string Code { get; set; } = string.Empty;
        public DateTime SubmittedAt { get; set; }
    }

}
