using BookShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.Data.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> book)
        {
            book
                .HasKey(b => b.BookId);

            book
                .Property(b => b.Title)
                .HasMaxLength(50)
                .IsRequired(true)
                .IsUnicode(true);

            book
                .Property(b => b.Description)
                .HasMaxLength(1000)
                .IsRequired(true)
                .IsUnicode(true);

            book
                .Property(b => b.ReleaseDate)
                .IsRequired(false);

            book
                .Property(b => b.Copies)
                .IsRequired(true);

            book
                .Property(b => b.Price)
                .IsRequired(true);

            book
                .Property(b => b.EditionType)
                .IsRequired(true);

            book
                .Property(b => b.AgeRestriction)
                .IsRequired(true);

            book
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}