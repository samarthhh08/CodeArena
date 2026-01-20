using Microsoft.EntityFrameworkCore;
using CjsApi.Models;

namespace CjsApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Problem> Problems => Set<Problem>();
        public DbSet<TestCase> TestCases => Set<TestCase>();
        public DbSet<Submission> Submissions => Set<Submission>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<ProblemTag> ProblemTags => Set<ProblemTag>();
        public DbSet<McqQuestion> McqQuestions => Set<McqQuestion>();
        public DbSet<McqAttempt> McqAttempts => Set<McqAttempt>();
        public DbSet<QuizSession> QuizSessions => Set<QuizSession>();

        public DbSet<Contest> Contests => Set<Contest>();

        public DbSet<ContestProblem> ContestProblems => Set<ContestProblem>();

        public DbSet<ContestParticipant> ContestParticipants => Set<ContestParticipant>();

        public DbSet<ContestSubmission> ContestSubmissions => Set<ContestSubmission>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Store enum as string
            modelBuilder.Entity<Problem>()
                .Property(p => p.Difficulty)
                .HasConversion<string>();

            // Unique Slug
            modelBuilder.Entity<Problem>()
                .HasIndex(p => p.Slug)
                .IsUnique();

            // Problem ↔ Tag (Many-to-Many)
            modelBuilder.Entity<ProblemTag>()
                .HasKey(pt => new { pt.ProblemId, pt.TagId });

            modelBuilder.Entity<ProblemTag>()
                .HasOne(pt => pt.Problem)
                .WithMany(p => p.ProblemTags)
                .HasForeignKey(pt => pt.ProblemId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProblemTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.ProblemTags)
                .HasForeignKey(pt => pt.TagId)
                .OnDelete(DeleteBehavior.Restrict);

            // Problem → TestCase
            modelBuilder.Entity<TestCase>()
                .HasOne(t => t.Problem)
                .WithMany(p => p.TestCases)
                .HasForeignKey(t => t.ProblemId)
                .OnDelete(DeleteBehavior.Cascade);

            // Problem → Submission
            modelBuilder.Entity<Submission>()
                .HasOne(s => s.Problem)
                .WithMany(p => p.Submissions)
                .HasForeignKey(s => s.ProblemId)
                .OnDelete(DeleteBehavior.Cascade);

            // MCQ Question → User (Creator)
            modelBuilder.Entity<McqQuestion>()
                .HasOne(m => m.Creator)
                .WithMany()
                .HasForeignKey(m => m.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // MCQ Attempt → User
            modelBuilder.Entity<McqAttempt>()
                .HasOne(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // MCQ Attempt → Question
            modelBuilder.Entity<McqAttempt>()
                .HasOne(m => m.Question)
                .WithMany(q => q.Attempts)
                .HasForeignKey(m => m.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            // MCQ Attempt → Quiz Session
            modelBuilder.Entity<McqAttempt>()
                .HasOne(m => m.QuizSession)
                .WithMany(q => q.Attempts)
                .HasForeignKey(m => m.QuizSessionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Quiz Session → User
            modelBuilder.Entity<QuizSession>()
                .HasOne(q => q.User)
                .WithMany()
                .HasForeignKey(q => q.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            // Composite PK
            modelBuilder.Entity<ContestProblem>()
                .HasKey(cp => new { cp.ContestId, cp.ProblemId });

            // ContestProblem → ContestSubmission
            modelBuilder.Entity<ContestSubmission>()
                .HasOne(cs => cs.ContestProblem)
                .WithMany(cp => cp.Submissions)
                .HasForeignKey(cs => new { cs.ContestId, cs.ProblemId });

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ContestParticipant>()
                .HasKey(cp => new { cp.ContestId, cp.UserId });

            modelBuilder.Entity<ContestSubmission>()
                .HasIndex(cs => new { cs.ContestId, cs.UserId });


        }
    }
}
