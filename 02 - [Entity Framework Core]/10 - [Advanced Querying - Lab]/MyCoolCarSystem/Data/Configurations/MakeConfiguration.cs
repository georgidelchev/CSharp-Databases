using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCoolCarSystem.Data.Models;

namespace MyCoolCarSystem.Data.Configurations
{
    public class MakeConfiguration : IEntityTypeConfiguration<Make>
    {
        public void Configure(EntityTypeBuilder<Make> make)
        {
            make
                .HasIndex(m => m.Name)
                .IsUnique();

            make
                .HasMany(m => m.Models)
                .WithOne(md => md.Make)
                .HasForeignKey(m => m.MakeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}