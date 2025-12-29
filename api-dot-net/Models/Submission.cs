using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CjsApi.Models
{


    public enum SubmissionStatus
    {
        PENDING,
        RUNNING,
        ACCEPTED,
        WRONG_ANSWER,
        TIME_LIMIT_EXCEEDED,
        RUNTIME_ERROR,
        COMPILATION_ERROR
    }

    public class Submission
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int ProblemId { get; set; }
        public Problem Problem { get; set; } = null!;

        [Required]
        public string Language { get; set; } = null!; // cpp, java, python

        [Required]
        public string Code { get; set; } = null!;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SubmissionStatus Status { get; set; }

        public int ExecutionTimeMs { get; set; }
        public int MemoryUsedKb { get; set; }

        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    }

   
}