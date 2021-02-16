using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Linq;
using System.Text;
using BookShop.Data;
using Newtonsoft.Json;
using System.Globalization;
using System.Xml.Serialization;
using BookShop.Data.Models.Enums;
using BookShop.DataProcessor.ExportDto;
using Formatting = Newtonsoft.Json.Formatting;

namespace BookShop.DataProcessor
{
    public class Serializer
    {
        public static string ExportMostCraziestAuthors(BookShopContext context)
        {
            var authors = context
                .Authors
                .Select(a => new
                {
                    AuthorName = a.FirstName + ' ' + a.LastName,
                    Books = a.AuthorsBooks
                        .OrderByDescending(b => b.Book.Price)
                        .Select(b => new
                        {
                            BookName = b.Book.Name,
                            BookPrice = b.Book.Price.ToString("f2")
                        })
                        .ToList()

                })
                .ToList()
                .OrderByDescending(a => a.Books.Count())
                .ThenBy(a => a.AuthorName)
                .ToList();

            var json = JsonConvert.SerializeObject(authors, Formatting.Indented);

            return json;
        }

        public static string ExportOldestBooks(BookShopContext context, DateTime date)
        {
            var sb = new StringBuilder();

            var books = context
                .Books
                .Where(b => b.Genre == Genre.Science && b.PublishedOn < date)
                .ToList()
                .OrderByDescending(b => b.Pages)
                .ThenByDescending(b => b.PublishedOn)
                .Select(b => new ExportBookDto()
                {
                    Name = b.Name,
                    Pages = b.Pages,
                    Date = b.PublishedOn.ToString("d", CultureInfo.InvariantCulture)
                })
                .Take(10)
                .ToList();

            var serializer = new XmlSerializer(typeof(List<ExportBookDto>), new XmlRootAttribute("Books"));

            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            var writer = new StringWriter(sb);

            using (writer)
            {
                serializer.Serialize(writer, books, namespaces);
            }

            return sb.ToString().Trim();
        }
    }
}