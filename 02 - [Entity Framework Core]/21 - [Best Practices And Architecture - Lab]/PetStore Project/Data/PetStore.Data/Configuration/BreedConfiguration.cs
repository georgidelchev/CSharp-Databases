using PetStore.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetStore.Data.Configuration
{
    using static DataValidations;

    public class BreedConfiguration : IEntityTypeConfiguration<Breed>
    {
        public void Configure(EntityTypeBuilder<Breed> breed)
        {
            breed
                .HasKey(b => b.Id);

            breed
                .Property(b => b.Name)
                .HasMaxLength(BREED_NAME_MAX_LENGTH)
                .IsRequired(true)
                .IsUnicode(true);
        }
    }
}