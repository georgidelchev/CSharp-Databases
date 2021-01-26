using System;
using System.IO;
using AutoMapper;
using System.Linq;
using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.Models;
using System.Collections.Generic;
using AutoMapper.QueryableExtensions;
using Newtonsoft.Json.Serialization;
using ProductShop.DTOs;

namespace ProductShop
{
    public class StartUp
    {
        private const string DATASETS_DIRECTORY_PATH = "../../../Datasets";

        private const string RESULTS_DIRECTORY_PATH = "../../../Datasets/Results";

        public static void Main(string[] args)
        {


            var db = new ProductShopContext();

            using (db)
            {
                InitializeMapper();
                // ResetDatabase(db);

                // Problem 01 - Import Users
                // var inputJson = File.ReadAllText($"{DATASETS_DIRECTORY_PATH}/users.json");

                // Console.WriteLine(ImportUsers(db, inputJson));

                // Problem 02 - Import Products
                // var inputJson = File.ReadAllText($"{DATASETS_DIRECTORY_PATH}/products.json");

                // Console.WriteLine(ImportProducts(db, inputJson));

                // Problem 03 - Import Categories
                // var inputJson = File.ReadAllText($"{DATASETS_DIRECTORY_PATH}/categories.json");

                // Console.WriteLine(ImportCategories(db, inputJson));

                // Problem 04 - Import Categories and Products
                // var inputJson = File.ReadAllText($"{DATASETS_DIRECTORY_PATH}/categories-products.json");

                // Console.WriteLine(ImportCategoryProducts(db, inputJson));

                // Problem 05 - Export Products in Range
                //var json = GetProductsInRange(db);

                //File.WriteAllText($"{RESULTS_DIRECTORY_PATH}/products-in-range.json", json);

                // Problem 06 - Export Successfully Sold Products
                //var json = GetSoldProducts(db);

                //File.WriteAllText($"{RESULTS_DIRECTORY_PATH}/users-sold-products.json", json);

                // Problem 07 - Export Categories by Products Count
                //var json = GetCategoriesByProductsCount(db);

                //File.WriteAllText($"{RESULTS_DIRECTORY_PATH}/categories-by-products.json", json);

                // Problem 08 - Export Users and Products
                var json = GetUsersWithProducts(db);

                File.WriteAllText($"{RESULTS_DIRECTORY_PATH}/users-and-products.json", json);
            }
        }

        // Problem 08 - Export Users and Products
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            using (context)
            {
                var users = context
                    .Users
                    .Where(u => u.ProductsSold.Count >= 1)
                    .Select(u => new
                    {
                        LastName = u.LastName,
                        Age = u.Age,
                        SoldProducts = new
                        {
                            Count = u.ProductsSold.Count(ps => ps.Buyer != null),
                            Products = u.ProductsSold
                              .Where(ps => ps.Buyer != null)
                              .Select(ps => new
                              {
                                  Name = ps.Name,
                                  Price = ps.Price
                              })
                              .ToList()
                        },
                    })
                    .OrderByDescending(u => u.SoldProducts.Count)
                    .ToList();

                var resultObj = new
                {
                    UsersCount = users.Count,
                    Users = users
                };

                var settings = new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    Formatting = Formatting.Indented,
                    ContractResolver = new DefaultContractResolver()
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    },
                    
                };

                var json = JsonConvert.SerializeObject(resultObj, settings);

                return json;
            }
        }

        // Problem 07 - Export Categories by Products Count
        public static string GetCategoriesByProductsCount(ProductShopContext
            context)
        {
            using (context)
            {
                var categories = context
                    .Categories
                    .ProjectTo<CategoriesByProductsCountDTO>()
                    .OrderByDescending(c => c.ProductsCount)
                    .ToList();

                var json = JsonConvert.SerializeObject(categories, Formatting.Indented);

                return json;
            }
        }

        // Problem 06 - Export Successfully Sold Products
        public static string GetSoldProducts(ProductShopContext context)
        {
            using (context)
            {
                var soldProducts = context
                    .Users
                    .Where(p => p.ProductsSold.Count >= 1)
                    .Select(u => new SuccessfullySoldProductsDTO()
                    {
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        SoldProducts = u.ProductsSold.Select(ps => new SuccessfullySoldProductsBuyerDTO()
                        {
                            Name = ps.Name,
                            Price = ps.Price,
                            BuyerFirstName = ps.Buyer.FirstName,
                            BuyerLastName = ps.Buyer.LastName
                        })
                         .ToList()
                    })
                    .OrderBy(u => u.LastName)
                    .ThenBy(u => u.FirstName)
                    .ToList();

                var json = JsonConvert.SerializeObject(soldProducts, Formatting.Indented);

                return json;
            }
        }

        // Problem 05 - Export Products in Range
        public static string GetProductsInRange(ProductShopContext context)
        {
            using (context)
            {
                var products = context
                    .Products
                    .Where(p => p.Price >= 500 &&
                                p.Price <= 1000)
                    .ProjectTo<ListProductsInRangeDTO>()
                    .OrderBy(p => p.Price)
                    .ToList();

                var json = JsonConvert.SerializeObject(products, Formatting.Indented);

                return json;
            }
        }

        // Problem 05 - Import Categories and Products
        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            using (context)
            {
                var categoryProducts = JsonConvert.DeserializeObject<List<CategoryProduct>>(inputJson);

                context.CategoryProducts.AddRange(categoryProducts);

                context.SaveChanges();

                return $"Successfully imported {categoryProducts.Count}";
            }
        }

        // Problem 04 - Import Categories
        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            using (context)
            {
                var categories = JsonConvert.DeserializeObject<List<Category>>(inputJson)
                    .Where(c => c.Name != null)
                    .ToList();

                context.Categories.AddRange(categories);

                context.SaveChanges();

                return $"Successfully imported {categories.Count}";
            }
        }

        // Problem 03 - Import Products
        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            using (context)
            {
                var products = JsonConvert.DeserializeObject<List<Product>>(inputJson);

                context.Products.AddRange(products);

                context.SaveChanges();

                return $"Successfully imported {products.Count}";
            }
        }

        // Problem 02 - Import Users
        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            using (context)
            {
                var serializerSettings = new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                };

                var usersDTO = JsonConvert.DeserializeObject<List<UserImportDTO>>(inputJson, serializerSettings);

                var users = usersDTO
                    .Select(udto => Mapper.Map<User>(udto))
                    .ToList();

                context.Users.AddRange(users);

                context.SaveChanges();

                return $"Successfully imported {users.Count}";
            }
        }

        // Reset Database to empty!
        private static void ResetDatabase(ProductShopContext db)
        {
            using (db)
            {
                db.Database.EnsureDeleted();
                Console.WriteLine("Db was successfully deleted!");

                db.Database.EnsureCreated();
                Console.WriteLine("Db was successfully created!");
            }
        }

        // Initializing the Mapper
        private static void InitializeMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            });
        }
    }
}