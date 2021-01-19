using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> student)
        {
            student
                .HasKey(s => s.StudentId);

            student
                .Property(s => s.Name)
                .HasMaxLength(100)
                .IsRequired(true)
                .IsUnicode(true);

            student
                .Property(s => s.PhoneNumber)
                .IsFixedLength()
                .HasColumnType("CHAR(10)")
                .IsRequired(false)
                .IsUnicode(false);

            student
                .Property(s => s.RegisteredOn)
                .IsRequired(true);

            student
                .Property(s => s.Birthday)
                .IsRequired(false);
        }
    }
}