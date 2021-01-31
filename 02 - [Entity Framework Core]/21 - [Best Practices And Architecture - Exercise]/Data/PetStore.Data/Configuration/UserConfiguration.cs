using PetStore.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetStore.Data.Configuration
{
    using static DataValidations;

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> user)
        {
            user
                .HasKey(u => u.Id);

            user
                .HasIndex(u => u.Email)
                .IsUnique();

            user
                .Property(u => u.Name)
                .HasMaxLength(USER_NAME_MAX_LENGTH)
                .IsRequired(true)
                .IsUnicode(true);

            user
                .Property(u => u.Email)
                .HasMaxLength(USER_EMAIL_MAX_LENGTH)
                .IsRequired(true)
                .IsUnicode(false);
        }
    }
}