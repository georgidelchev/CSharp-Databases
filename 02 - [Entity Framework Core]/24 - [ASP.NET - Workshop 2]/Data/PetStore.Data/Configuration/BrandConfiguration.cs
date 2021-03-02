using PetStore.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetStore.Data.Configuration
{
    using static DataValidations;

    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> brand)
        {
            brand
                .HasKey(b => b.Id);

            brand
                .Property(b => b.Name)
                .HasMaxLength(BRAND_NAME_MAX_LENGTH)
                .IsRequired(true)
                .IsUnicode(true);
        }
    }
}