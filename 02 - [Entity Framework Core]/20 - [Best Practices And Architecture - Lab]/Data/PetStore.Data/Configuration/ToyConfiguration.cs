using PetStore.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetStore.Data.Configuration
{
    using static DataValidations;

    public class ToyConfiguration : IEntityTypeConfiguration<Toy>
    {
        public void Configure(EntityTypeBuilder<Toy> toy)
        {
            toy
                .HasKey(t => t.Id);

            toy
                .Property(t => t.Name)
                .HasMaxLength(TOY_MAX_LENGTH)
                .IsRequired(true)
                .IsUnicode(true);

            toy
                .Property(t => t.Description)
                .HasMaxLength(TOY_DESCRIPTION_MAX_LENGTH)
                .IsRequired(false)
                .IsUnicode(true);

            toy
                .Property(t => t.Price)
                .IsRequired(true);

            toy
                .HasOne(t => t.Brand)
                .WithMany(b => b.Toys)
                .HasForeignKey(t => t.BrandId)
                .OnDelete(DeleteBehavior.Restrict);

            toy
                .HasOne(t => t.Category)
                .WithMany(c => c.Toys)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}