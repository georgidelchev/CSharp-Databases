using System;
using AutoMapper;
using System.Linq;
using System.Text;
using BookShop.Data;
using Newtonsoft.Json;
using BookShop.Models;
using System.Globalization;
using AutoMapper.Collection;
using BookShop.Models.Enums;
using BookShop.Data.ViewModels;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using AutoMapper.EquivalencyExpression;

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

                // ManualMapping(book, db);

                // ManualMapping2(db);

                Mapper.Initialize(cfg =>
                {
                    cfg.AddCollectionMappers();

                    cfg.CreateMap<Book, BookDTO>()
                        .ReverseMap();

                    cfg.CreateMap<BookDTO, Book>()
                        .EqualityComparison((dto, obj) => dto.BookId == obj.BookId);
                });

                //var book = db
                //    .Books
                //    .Include(b => b.Author)
                //    .First();

                //var bookDto = Mapper.Map<BookDTO>(book);

                //var bookDto = new BookDTO()
                //{
                //    Title = "Title",
                //    Price = 10m,
                //    Author = "Pesho"
                //};

                //var book = Mapper.Map<Book>(bookDto);

                //var result = JsonConvert.SerializeObject(book, Formatting.Indented);

                //Console.WriteLine(result);

                //var books = db
                //    .Books
                //    .ProjectTo<BookDTO>()
                //    .ToList();

                var books = new List<Book>()
                {
                    new Book()
                    {
                        BookId = 1,
                        Title = "Title1",
                        Price = 100m
                    },

                    new Book()
                    {
                        BookId = 3,
                        Title = "Title3",
                        Price = 300m
                    }
                };

                var bookDtos = new List<BookDTO>()
                {
                    new BookDTO()
                    {
                        BookId = 1,
                        Title = "Title1"
                    },

                    new BookDTO()
                    {
                        BookId = 2,
                        Title = "Title3",
                        Price = 15
                    }
                };

                Mapper.Map<List<BookDTO>, List<Book>>(bookDtos, books);

                var result = JsonConvert.SerializeObject(books, Formatting.Indented);

                Console.WriteLine(result);
            }
        }

        //private static void ManualMapping2(BookShopContext db)
        //{
        //    var books = db
        //         .Books
        //         .Select(b => new BookDTO()
        //         {
        //             Title = b.Title,
        //             Price = b.Price,
        //             Author = $"{b.Author.FirstName} " +
        //                      $"{b.Author.LastName}"
        //         })
        //          .ToList();

        //    var result = JsonConvert.SerializeObject(books, Formatting.Indented);

        //    Console.WriteLine(result);
        //}

        //private static void ManualMapping(Book book, BookShopContext db)
        //{
        //    book = db
        //       .Books
        //       .Include(b => b.Author)
        //       .First();

        //    var bookDto = new BookDTO()
        //    {
        //        Title = book.Title,
        //        Price = book.Price,
        //        Author = $"{book.Author.FirstName} " +
        //                                 $"{book.Author.LastName}"
        //    };

        //    var result = JsonConvert.SerializeObject(bookDto, Formatting.Indented);

        //    Console.WriteLine(result);
        //}
    }
}
