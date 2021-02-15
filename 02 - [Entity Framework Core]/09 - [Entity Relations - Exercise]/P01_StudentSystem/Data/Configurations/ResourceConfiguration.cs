using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data
{
    public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> resource)
        {
            resource
                .HasKey(r => r.ResourceId);

            resource
                .Property(r => r.Name)
                .HasMaxLength(50)
                .IsRequired(true)
                .IsUnicode(true);

            resource
                .Property(r => r.Url)
                .IsRequired(true)
                .IsUnicode(false);

            resource
                .Property(r => r.ResourceType)
                .IsRequired(true);

            resource
                .HasOne(r => r.Course)
                .WithMany(c => c.Resources)
                .HasForeignKey(r => r.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}