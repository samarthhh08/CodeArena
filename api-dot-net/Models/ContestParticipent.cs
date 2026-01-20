namespace CjsApi.Models
{
    public class ContestParticipant
    {
        public int ContestId { get; set; }
        public Contest Contest { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
        public bool IsDisqualified { get; set; } = false;
    }
}
