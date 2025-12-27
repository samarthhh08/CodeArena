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
        }
    }
}
