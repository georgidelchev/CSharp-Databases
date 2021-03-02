using PetStore.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetStore.Data.Configuration
{
    using static DataValidations;

    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> category)
        {
            category
                .HasKey(c => c.Id);

            category
                .Property(c => c.Name)
                .HasMaxLength(CATEGORY_NAME_MAX_LENGTH)
                .IsRequired(true)
                .IsUnicode(true);

            category
                .Property(c => c.Description)
                .HasMaxLength(CATEGORY_DESCRIPTION_MAX_LENGTH)
                .IsRequired(false)
                .IsUnicode(true);
        }
    }
}