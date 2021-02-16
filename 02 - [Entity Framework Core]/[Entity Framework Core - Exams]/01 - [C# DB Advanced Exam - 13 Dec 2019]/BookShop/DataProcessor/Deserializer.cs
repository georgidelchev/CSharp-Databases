using System;
using System.IO;
using System.Linq;
using System.Text;
using BookShop.Data;
using Newtonsoft.Json;
using BookShop.Data.Models;
using System.Globalization;
using System.Xml.Serialization;
using System.Collections.Generic;
using BookShop.Data.Models.Enums;
using BookShop.DataProcessor.ImportDto;
using System.ComponentModel.DataAnnotations;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace BookShop.DataProcessor
{
    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedBook
            = "Successfully imported book {0} for {1:F2}.";

        private const string SuccessfullyImportedAuthor
            = "Successfully imported author - {0} with {1} books.";

        public static string ImportBooks(BookShopContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var xmlSerializer = new XmlSerializer(typeof(List<ImportBookDto>), new XmlRootAttribute("Books"));

            var reader = new StringReader(xmlString);

            using (reader)
            {
                var bookDtos = (List<ImportBookDto>)xmlSerializer.Deserialize(reader);

                var booksToAdd = new List<Book>();

                foreach (var bookDto in bookDtos)
                {
                    if (!IsValid(bookDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    DateTime publishedOn;

                    var isDateValid = DateTime.TryParseExact(bookDto.PublishedOn, "MM/dd/yyyy",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out publishedOn); ;

                    if (!isDateValid)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    var validBook = new Book()
                    {
                        Name = bookDto.Name,
                        Genre = (Genre)bookDto.Genre,
                        Price = bookDto.Price,
                        Pages = bookDto.Pages,
                        PublishedOn = publishedOn
                    };

                    booksToAdd.Add(validBook);

                    sb.AppendLine(string.Format(SuccessfullyImportedBook, validBook.Name, validBook.Price));
                }

                context.Books.AddRange(booksToAdd);

                context.SaveChanges();
            }

            return sb.ToString().Trim();
        }

        public static string ImportAuthors(BookShopContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var authorDtos = JsonConvert.DeserializeObject<List<ImportAuthorDto>>(jsonString);

            var authorsToAdd = new List<Author>();

            foreach (var authorDto in authorDtos)
            {
                if (!IsValid(authorDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (authorsToAdd.Any(a => a.Email == authorDto.Email))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var author = new Author()
                {
                    FirstName = authorDto.FirstName,
                    LastName = authorDto.LastName,
                    Email = authorDto.Email,
                    Phone = authorDto.Phone
                };

                foreach (var bookDto in authorDto.Books)
                {
                    if (!bookDto.BookId.HasValue)
                    {
                        continue;
                    }

                    var book = context
                        .Books
                        .FirstOrDefault(b => b.Id == bookDto.BookId);

                    if (book == null)
                    {
                        continue;
                    }

                    author.AuthorsBooks.Add(new AuthorBook()
                    {
                        Author = author,
                        Book = book
                    });
                }

                if (author.AuthorsBooks.Count == 0)
                {
                    sb.AppendLine(ErrorMessage);

                    continue;
                }

                authorsToAdd.Add(author);

                sb.AppendLine(string.Format(SuccessfullyImportedAuthor, author.FirstName + ' ' + author.LastName,
                    author.AuthorsBooks.Count));
            }

            context.Authors.AddRange(authorsToAdd);

            context.SaveChanges();

            return sb.ToString().Trim();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}