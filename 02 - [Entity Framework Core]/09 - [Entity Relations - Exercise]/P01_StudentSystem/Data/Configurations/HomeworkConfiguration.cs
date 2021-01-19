using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data
{
    public class HomeworkConfiguration : IEntityTypeConfiguration<Homework>
    {
        public void Configure(EntityTypeBuilder<Homework> homework)
        {
            homework
                .HasKey(h => h.HomeworkId);

            homework
                .Property(h => h.Content)
                .IsRequired(true)
                .IsUnicode(false);

            homework
                .Property(h => h.ContentType)
                .IsRequired(true)
                .IsUnicode(true);

            homework
                .Property(h => h.SubmissionTime)
                .IsRequired(true);

            homework
                .HasOne(h => h.Student)
                .WithMany(s => s.HomeworkSubmissions)
                .HasForeignKey(h => h.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            homework
                .HasOne(h => h.Course)
                .WithMany(c => c.HomeworkSubmissions)
                .HasForeignKey(h => h.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}