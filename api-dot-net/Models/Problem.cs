using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace CjsApi.Models
{
    public enum Difficulty
    {
        EASY,
        MEDIUM,
        HARD
    }



    [Index(nameof(Slug), IsUnique = true)]
    public class Problem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(150)]
        public string Slug { get; set; } = null!; // e.g. two-sum

        [Required]
        [Column(TypeName = "LONGTEXT")]
        public string Description { get; set; } = null!;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Difficulty Difficulty { get; set; }

        public int TimeLimitMs { get; set; }
        public int MemoryLimitMb { get; set; }

        public bool IsPublished { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // 🔗 Relationships
        public ICollection<TestCase> TestCases { get; set; } = new List<TestCase>();
        public ICollection<Submission> Submissions { get; set; } = new List<Submission>();
        public ICollection<ProblemTag> ProblemTags { get; set; } = new List<ProblemTag>();
    }
}
