using BookShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.Data.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> author)
        {
            author
                .HasKey(a => a.AuthorId);

            author
                .Property(a => a.FirstName)
                .HasMaxLength(50)
                .IsRequired(false)
                .IsUnicode(true);

            author
                .Property(a => a.LastName)
                .HasMaxLength(50)
                .IsRequired(true)
                .IsUnicode(true);
        }
    }
}