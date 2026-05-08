using Assessment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assessment.Infrastructure.Data
{
    public class AssessmentDbContext : DbContext
    {
        public AssessmentDbContext(DbContextOptions<AssessmentDbContext> options)
            : base(options)
        {
        }

        public DbSet<Question> Questions => Set<Question>();
        public DbSet<Choice> Choices => Set<Choice>();
        public DbSet<AssessmentResult> AssessmentResults => Set<AssessmentResult>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("assessment_questions");
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Id).HasColumnName("id");
                entity.Property(x => x.Text).HasColumnName("question_text");
                entity.Property(x => x.CorrectChoiceId).HasColumnName("correct_choice_id");

                entity.HasMany(x => x.Choices)
                    .WithOne()
                    .HasForeignKey("QuestionId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Choice>(entity =>
            {
                entity.ToTable("assessment_choices");
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Id).HasColumnName("id");
                entity.Property(x => x.Text).HasColumnName("choice_text");

                entity.Property<int>("QuestionId").HasColumnName("question_id");
            });

            modelBuilder.Entity<AssessmentResult>(entity =>
            {
                entity.ToTable("assessment_results");
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Id).HasColumnName("id");
                entity.Property(x => x.FullName).HasColumnName("full_name");
                entity.Property(x => x.Score).HasColumnName("score");
                entity.Property(x => x.Total).HasColumnName("total");
                entity.Property(x => x.SubmittedAt).HasColumnName("submitted_at");
            });
        }
    }
}
