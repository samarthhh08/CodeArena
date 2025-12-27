using System.ComponentModel.DataAnnotations;

namespace CjsApi.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!; // Array, DP, Graph

        // 🔗 Many-to-Many
        public ICollection<ProblemTag> ProblemTags { get; set; } = new List<ProblemTag>();
    }


    public class ProblemTag
    {
        public int ProblemId { get; set; }
        public Problem Problem { get; set; } = null!;

        public int TagId { get; set; }
        public Tag Tag { get; set; } = null!;
    }
}
