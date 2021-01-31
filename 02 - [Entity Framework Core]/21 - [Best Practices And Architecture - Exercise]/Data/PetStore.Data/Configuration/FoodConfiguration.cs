using PetStore.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetStore.Data.Configuration
{
    using static DataValidations;

    public class FoodConfiguration : IEntityTypeConfiguration<Food>
    {
        public void Configure(EntityTypeBuilder<Food> food)
        {
            food
                .HasKey(f => f.Id);

            food
                .Property(f => f.Name)
                .HasMaxLength(FOOD_NAME_MAX_LENGTH)
                .IsRequired(true)
                .IsUnicode(true);

            food
                .Property(f => f.Weight)
                .IsRequired(true);

            food
                .Property(f => f.DistributorPrice)
                .IsRequired(true);

            food
                .Property(f => f.Price)
                .IsRequired(true);

            food
                .Property(f => f.ExpirationDate)
                .IsRequired(true);

            food
                .HasOne(f => f.Brand)
                .WithMany(b => b.Foods)
                .HasForeignKey(f => f.BrandId)
                .OnDelete(DeleteBehavior.Restrict);

            food
                .HasOne(f => f.Category)
                .WithMany(c => c.Foods)
                .HasForeignKey(f => f.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}