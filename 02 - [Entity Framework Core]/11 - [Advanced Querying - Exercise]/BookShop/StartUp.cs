using System;
using System.Linq;
using System.Text;
using BookShop.Data;
using System.Globalization;
using BookShop.Models.Enums;

namespace BookShop
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var db = new BookShopContext();

            using (db)
            {
                // DbInitializer.ResetDatabase(db);

                // Problem 02 - Age Restriction
                // var command = Console.ReadLine();
                // Console.WriteLine(GetBooksByAgeRestriction(db, command));


                // Problem 03 - Golden Books
                // Console.WriteLine(GetGoldenBooks(db));


                // Problem 04 - Books by Price
                // Console.WriteLine(GetBooksByPrice(db));


                // Problem 05 - Not Released In
                // var year = int.Parse(Console.ReadLine());
                // Console.WriteLine(GetBooksNotReleasedIn(db, year));


                // Problem 06 - Book Titles by Category
                // var input = Console.ReadLine();
                // Console.WriteLine(GetBooksByCategory(db, input));


                // Problem 07 - Released Before Date
                // var date = Console.ReadLine();
                // Console.WriteLine(GetBooksReleasedBefore(db, date));


                // Problem 08 - Author Search
                // var input = Console.ReadLine();
                // Console.WriteLine(GetAuthorNamesEndingIn(db, input));


                // Problem 09 - Book Search
                // var input = Console.ReadLine();
                // Console.WriteLine(GetBookTitlesContaining(db, input));


                // Problem 10 - Book Search by Author
                //var input = Console.ReadLine();
                //Console.WriteLine(GetBooksByAuthor(db, input));


                // Problem 11 - Count Books
                // var input = int.Parse(Console.ReadLine());
                // Console.WriteLine(CountBooks(db, input));


                // Problem 12 - Total Book Copies
                // Console.WriteLine(CountCopiesByAuthor(db));


                // Problem 13 - Profit by Category
                // Console.WriteLine(GetTotalProfitByCategory(db));


                // Problem 14 - Most Recent Books
                // Console.WriteLine(GetMostRecentBooks(db));


                // Problem 15 - Increase Prices
                // IncreasePrices(db);


                // Problem 16 - Remove Books
                // Console.WriteLine(RemoveBooks(db));
            }
        }

        // Problem 16 - Remove Books
        public static int RemoveBooks(BookShopContext context)
        {
            var books = context
                .Books
                .Where(b => b.Copies < 4200);

            var deletedFields = books.Count();

            var bookCategories = context
                .BookCategories
                .Where(bc => bc.Book.Copies < 4200);

            context.BookCategories.RemoveRange(bookCategories);

            context.Books.RemoveRange(books);

            context.SaveChanges();

            return deletedFields;
        }

        // Problem 15 - Increase Prices
        public static void IncreasePrices(BookShopContext context)
        {
            var bookPrices = context
                .Books
                .Where(b => b.ReleaseDate.Value.Year < 2010)
                .ToList();

            foreach (var bookPrice in bookPrices)
            {
                bookPrice.Price += 5;
            }

            context.SaveChanges();
        }

        // Problem 14 - Most Recent Books
        public static string GetMostRecentBooks(BookShopContext context)
        {
            var sb = new StringBuilder();

            using (context)
            {
                var categories = context
                    .Categories
                    .Select(c => new
                    {
                        Name = c.Name,
                        Books = c.CategoryBooks.Select(cb => new
                        {
                            Title = cb.Book.Title,
                            ReleaseDate = cb.Book.ReleaseDate
                        })
                         .OrderByDescending(cb => cb.ReleaseDate)
                         .Take(3)
                         .ToList()
                    })
                    .OrderBy(c => c.Name)
                    .ToList();

                foreach (var category in categories)
                {
                    sb.AppendLine($"--{category.Name}");

                    foreach (var book in category.Books)
                    {
                        sb.AppendLine($"{book.Title} " + $"({book.ReleaseDate.Value.Year})");
                    }
                }
            }

            return sb.ToString().Trim();
        }

        // Problem 13 - Profit by Category
        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var sb = new StringBuilder();

            using (context)
            {
                var categories = context
                    .Categories
                    .Select(c => new
                    {
                        Name = c.Name,
                        Profit = c.CategoryBooks
                            .Sum(cb => cb.Book.Copies * cb.Book.Price)
                    })
                    .OrderByDescending(c => c.Profit)
                    .ThenBy(c => c.Name)
                    .ToList();

                foreach (var category in categories)
                {
                    sb.AppendLine($"{category.Name} " +
                                  $"${category.Profit:f2}");
                }
            }

            return sb.ToString().Trim();
        }

        // Problem 12 - Total Book Copies
        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var sb = new StringBuilder();

            using (context)
            {
                var authorBooks = context
                    .Authors
                    .Select(a => new
                    {
                        FirstName = a.FirstName,
                        LastName = a.LastName,
                        BooksCount = a.Books
                                      .Sum(b => b.Copies)
                    })
                    .OrderByDescending(b => b.BooksCount)
                    .ToList();

                foreach (var author in authorBooks)
                {
                    sb.AppendLine($"{author.FirstName} " +
                                  $"{author.LastName} - " +
                                  $"{author.BooksCount}");
                }
            }

            return sb.ToString().Trim();
        }

        // Problem 11 - Count Books
        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            using (context)
            {
                var booksCount = context
                    .Books
                    .Count(b => b.Title.Length > lengthCheck);

                return booksCount;
            }
        }

        // Problem 10 - Book Search by Author
        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var sb = new StringBuilder();

            using (context)
            {
                var books = context
                    .Books
                    .OrderBy(b => b.BookId)
                    .Select(b => new
                    {
                        Title = b.Title,
                        AuthorFirstName = b.Author.FirstName,
                        AuthorLastName = b.Author.LastName
                    })
                    .Where(a => a.AuthorLastName.ToLower().StartsWith(input.ToLower()))
                    .ToList();

                foreach (var book in books)
                {
                    sb.AppendLine($"{book.Title} " +
                                  $"({book.AuthorFirstName} " +
                                  $"{book.AuthorLastName})");
                }
            }

            return sb.ToString().Trim();
        }

        // Problem 09 - Book Search
        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var sb = new StringBuilder();

            using (context)
            {
                var books = context
                    .Books
                    .Where(b => b.Title.ToLower().Contains(input.ToLower()))
                    .OrderBy(b => b.Title)
                    .Select(b => b.Title)
                    .ToList();

                foreach (var book in books)
                {
                    sb.AppendLine(book);
                }
            }

            return sb.ToString().Trim();
        }

        // Problem 08 - Author Search
        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var sb = new StringBuilder();

            using (context)
            {
                var authors = context
                    .Authors
                    .Where(a => a.FirstName.EndsWith(input))
                    .Select(a => new
                    {
                        FullName = a.FirstName + " " + a.LastName
                    })
                    .ToList();

                foreach (var author in authors)
                {
                    sb.AppendLine(author.FullName);
                }
            }

            return sb.ToString().Trim();
        }
        // Problem 07 - Released Before Date
        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            var sb = new StringBuilder();

            using (context)
            {
                var formattedDate = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);

                var books = context
                    .Books
                    .Where(b => b.ReleaseDate < formattedDate)
                    .OrderByDescending(b => b.ReleaseDate)
                    .Select(b => new
                    {
                        Title = b.Title,
                        EditionType = b.EditionType,
                        Price = b.Price
                    })
                    .ToList();

                foreach (var book in books)
                {
                    sb.AppendLine($"{book.Title} - " +
                                  $"{book.EditionType} - " +
                                  $"${book.Price:f2}");
                }
            }

            return sb.ToString().Trim();
        }

        // Problem 06 - Book Titles by Category
        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            var sb = new StringBuilder();

            using (context)
            {
                var categories = input
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(c => c.ToLower())
                    .ToList();

                var books = context
                    .Books
                    .Where(b => b.BookCategories
                                 .Any(bc => categories.Contains(bc.Category.Name)))
                    .Select(b => b.Title)
                    .OrderBy(b => b)
                    .ToList();

                foreach (var book in books)
                {
                    sb.AppendLine(book);
                }
            }

            return sb.ToString().Trim();
        }

        // Problem 05 - Not Released In
        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            var sb = new StringBuilder();

            using (context)
            {
                var books = context
                    .Books
                    .Where(b => b.ReleaseDate.Value.Year != year)
                    .OrderBy(b => b.BookId)
                    .Select(b => b.Title)
                    .ToList();

                foreach (var book in books)
                {
                    sb.AppendLine($"{book}");
                }
            }

            return sb.ToString().Trim();
        }

        // Problem 04 - Books by Price
        public static string GetBooksByPrice(BookShopContext context)
        {
            var sb = new StringBuilder();

            using (context)
            {
                var books = context
                    .Books
                    .Select(b => new
                    {
                        Title = b.Title,
                        Price = b.Price
                    })
                    .Where(b => b.Price > 40)
                    .OrderByDescending(b => b.Price)
                    .ToList();

                foreach (var book in books)
                {
                    sb.AppendLine($"{book.Title} - " +
                                  $"${book.Price:f2}");
                }
            }

            return sb.ToString().Trim();
        }

        // Problem 03 - Golden Books
        public static string GetGoldenBooks(BookShopContext context)
        {
            var sb = new StringBuilder();

            using (context)
            {
                var books = context
                    .Books
                    .Where(b => b.Copies < 5000 && b.EditionType == EditionType.Gold)
                    .OrderBy(b => b.BookId)
                    .Select(b => b.Title)
                    .ToList();

                foreach (var book in books)
                {
                    sb.AppendLine(book);
                }
            }

            return sb.ToString().Trim();
        }

        // Problem 02 - Age Restriction
        public static string GetBooksByAgeRestriction(BookShopContext context,
            string command)
        {
            var sb = new StringBuilder();

            using (context)
            {
                var books = context
                    .Books
                    .Where(b => b.AgeRestriction == Enum.Parse<AgeRestriction>(command, true))
                    .Select(b => b.Title)
                    .OrderBy(b => b)
                    .ToList();

                foreach (var book in books)
                {
                    sb.AppendLine($"{book}");
                }
            }

            return sb.ToString().Trim();
        }
    }
}
