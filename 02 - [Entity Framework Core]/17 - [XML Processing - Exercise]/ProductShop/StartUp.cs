using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ProductShop.Data;
using ProductShop.Dtos.Export;
using ProductShop.Dtos.Import;
using ProductShop.Models;

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
                // TODO ResetDatabase(db);

                // TODO Initialize Mapper
                InitializeMapper();

                // TODO Problem 01 - Import Users
                //var inputXml = File.ReadAllText($"{DATASETS_DIRECTORY_PATH}/users.xml");

                //Console.WriteLine(ImportUsers(db, inputXml));

                // TODO Problem 02 - Import Products
                //var inputXml = File.ReadAllText($"{DATASETS_DIRECTORY_PATH}/products.xml");

                //Console.WriteLine(ImportProducts(db, inputXml));

                // TODO Problem 03 - Import Categories
                //var inputXml = File.ReadAllText($"{DATASETS_DIRECTORY_PATH}/categories.xml");

                //Console.WriteLine(ImportCategories(db, inputXml));

                // TODO Problem 04 - Import Categories and Products
                //var inputXml = File.ReadAllText($"{DATASETS_DIRECTORY_PATH}/categories-products.xml");

                //Console.WriteLine(ImportCategoryProducts(db, inputXml));

                // TODO Problem 05 - Export Products In Range
                //var result = GetProductsInRange(db);

                //File.WriteAllText($"{RESULTS_DIRECTORY_PATH}/products-in-range.xml", result);

                // TODO Problem 06 - Export Sold Products
                //var result = GetSoldProducts(db);

                //File.WriteAllText($"{RESULTS_DIRECTORY_PATH}/users-sold-products.xml", result);

                // TODO Problem 07 - Export Get Categories By Products Count
                //var result = GetCategoriesByProductsCount(db);

                //File.WriteAllText($"{RESULTS_DIRECTORY_PATH}/categories-by-products.xml", result);

                // TODO Problem 08 - Export Users and Products
                var result = GetUsersWithProducts(db);

                File.WriteAllText($"{RESULTS_DIRECTORY_PATH}/users-and-products.xml", result);
            }
        }

        // TODO Problem 08 - Export Users and Products
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var sb = new StringBuilder();

            using (context)
            {
                var users = new UserRootDTO()
                {

                    Count = context.Users.Count(u => u.FirstName != null),
                    Users = context
                        .Users
                        .Where(u => u.ProductsSold.Count >= 1)
                        .OrderByDescending(u => u.ProductsSold.Count)
                        .Select(u => new UserExportDTO()
                        {
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            Age = u.Age,
                            SoldProducts = new ProductSoldRootDTO()
                            {
                                Count = u.ProductsSold.Count(ps => ps.Buyer != null),
                                Products = u.ProductsSold
                                    .Where(ps => ps.Buyer != null)
                                    .Select(s => new ProductSoldDTO()
                                    {
                                        Name = s.Name,
                                        Price = s.Price
                                    })
                                    .ToList()
                            }
                        })
                        .ToList()
                };

                var xmlSerializer = new XmlSerializer(typeof(UserRootDTO), new XmlRootAttribute("Users"));

                var namespaces = new XmlSerializerNamespaces();

                namespaces.Add("", "");

                var writer = new StringWriter(sb);

                using (writer)
                {
                    xmlSerializer.Serialize(writer, users, namespaces);
                }
            }

            return sb.ToString().Trim();
        }

        // TODO Problem 07 - Export Get Categories By Products Count
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var sb = new StringBuilder();

            using (context)
            {
                var categories = context
                    .Categories
                    .ProjectTo<ExportCategoriesByProductsCountDTO>()
                    .OrderByDescending(c => c.Count)
                    .ThenBy(c => c.TotalRevenue)
                    .ToList();

                var xmlSerializer =
                    new XmlSerializer(
                        typeof(List<ExportCategoriesByProductsCountDTO>), new XmlRootAttribute("Categories"));

                var namespaces = new XmlSerializerNamespaces();

                namespaces.Add("", "");

                var writer = new StringWriter(sb);

                using (writer)
                {
                    xmlSerializer.Serialize(writer, categories, namespaces);
                }
            }

            return sb.ToString().Trim();
        }

        // TODO Problem 06 - Export Sold Products
        public static string GetSoldProducts(ProductShopContext context)
        {
            var sb = new StringBuilder();

            using (context)
            {
                var users = context
                    .Users
                    .ProjectTo<ExportUserDTO>()
                    .Where(u => u.SoldProducts.Count >= 1)
                    .OrderBy(u => u.LastName)
                    .ThenBy(u => u.FirstName)
                    .Take(5)
                    .ToList();

                var xmlSerializer = new XmlSerializer(typeof(List<ExportUserDTO>), new XmlRootAttribute("Users"));

                var namespaces = new XmlSerializerNamespaces();

                namespaces.Add("", "");

                var writer = new StringWriter(sb);

                using (writer)
                {
                    xmlSerializer.Serialize(writer, users, namespaces);
                }
            }

            return sb.ToString().Trim();
        }

        // TODO Problem 05 - Export Products In Range
        public static string GetProductsInRange(ProductShopContext context)
        {
            var sb = new StringBuilder();

            using (context)
            {
                var products = context
                    .Products
                    .Where(p => p.Price >= 500 &&
                                p.Price <= 1000)
                    .OrderBy(p => p.Price)
                    .ProjectTo<ExportProductsInRangeDTO>
                        ()
                    .Take(10)
                    .ToList();

                var xmlSerializer = new XmlSerializer(typeof(List<ExportProductsInRangeDTO>),
                    new XmlRootAttribute("Products"));

                var namespaces = new XmlSerializerNamespaces();

                namespaces.Add("", "");

                var writer = new StringWriter(sb);

                using (writer)
                {
                    xmlSerializer.Serialize(writer, products, namespaces);
                }
            }

            return sb.ToString().Trim();
        }

        // TODO Problem 04 - Import Categories Products
        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            using (context)
            {
                var xmlSerializer = new XmlSerializer(typeof(List<ImportCategoryProductDTO>),
                    new XmlRootAttribute("CategoryProducts"));

                var reader = new StringReader(inputXml);

                using (reader)
                {
                    var categoryProductDtos = (List<ImportCategoryProductDTO>)xmlSerializer.Deserialize(reader);

                    var categoryProducts = Mapper.Map<List<CategoryProduct>>(categoryProductDtos)
                        .Where(cp => context.Categories.Any(c => c.Id == cp.CategoryId))
                        .Where(cp => context.Products.Any(p => p.Id == cp.ProductId))
                        .ToList();

                    context.AddRange(categoryProducts);

                    context.SaveChanges();

                    return $"Successfully imported {categoryProducts.Count}";
                }
            }
        }

        // TODO Problem 03 - Import Categories
        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            using (context)
            {
                var xmlSerializer =
                    new XmlSerializer(typeof(List<ImportCategoryDTO>), new XmlRootAttribute("Categories"));

                var reader = new StringReader(inputXml);

                using (reader)
                {
                    var categoryDtos = (List<ImportCategoryDTO>)xmlSerializer.Deserialize(reader);

                    var categories = Mapper.Map<List<Category>>(categoryDtos)
                        .Where(c => c.Name != null)
                        .ToList();

                    context.AddRange(categories);

                    context.SaveChanges();

                    return $"Successfully imported {categories.Count}";
                }
            }
        }

        // TODO Problem 02 - Import Products
        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            using (context)
            {
                var xmlSerializer = new XmlSerializer(typeof(List<ImportProductDTO>), new XmlRootAttribute("Products"));

                var reader = new StringReader(inputXml);

                using (reader)
                {
                    var productDtos = (List<ImportProductDTO>)xmlSerializer.Deserialize(reader);

                    var products = Mapper.Map<List<Product>>(productDtos);

                    context.AddRange(products);

                    context.SaveChanges();

                    return $"Successfully imported {products.Count}";
                }
            }
        }

        // TODO Problem 01 - Import Users
        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            using (context)
            {
                var xmlSerializer = new XmlSerializer(typeof(List<ImportUserDTO>), new XmlRootAttribute("Users"));

                var reader = new StringReader(inputXml);

                using (reader)
                {
                    var userDtos = (List<ImportUserDTO>)xmlSerializer.Deserialize(reader);

                    var users = Mapper.Map<List<User>>(userDtos);

                    context.AddRange(users);

                    context.SaveChanges();

                    return $"Successfully imported {users.Count}";
                }
            }
        }

        // TODO Initializing the Mapper
        private static void InitializeMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            });
        }

        // TODO Reset Database to empty!
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
    }
}