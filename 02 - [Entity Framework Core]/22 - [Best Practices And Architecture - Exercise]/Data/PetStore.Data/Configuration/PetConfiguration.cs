using PetStore.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetStore.Data.Configuration
{
    using static DataValidations;

    public class PetConfiguration : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> pet)
        {
            pet
                .HasKey(p => p.Id);

            pet
                .Property(p => p.Name)
                .HasMaxLength(PET_NAME_MAX_LENGTH)
                .IsRequired(true)
                .IsUnicode(true);

            pet
                .Property(p => p.Gender)
                .IsRequired(true);

            pet
                .Property(p => p.DateOfBirth)
                .IsRequired(true);

            pet
                .Property(p => p.Price)
                .IsRequired(true);

            pet
                .Property(p => p.Description)
                .IsRequired(false)
                .IsUnicode(true);

            pet
                .HasOne(p => p.Breed)
                .WithMany(b => b.Pets)
                .HasForeignKey(p => p.BreedId)
                .OnDelete(DeleteBehavior.Restrict);

            pet
                .HasOne(p => p.Category)
                .WithMany(c => c.Pets)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            pet
                .HasOne(p => p.Category)
                .WithMany(c => c.Pets)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            pet
                .HasOne(p => p.Order)
                .WithMany(o => o.Pets)
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}