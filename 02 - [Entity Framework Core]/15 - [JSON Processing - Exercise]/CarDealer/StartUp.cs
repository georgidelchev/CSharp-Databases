using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarDealer.Data;
using CarDealer.DTO;
using CarDealer.Models;
using Newtonsoft.Json;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace CarDealer
{
    public class StartUp
    {
        private const string DATASETS_DIRECTORY_PATH = "../../../Datasets";

        private const string RESULTS_DIRECTORY_PATH = "../../../Datasets/Results";

        public static void Main(string[] args)
        {
            var db = new CarDealerContext();

            using (db)
            {
                InitializeMapper();

                // ResetDatabase(db);

                // Problem 01 - Import Suppliers
                // var jsonInput = File.ReadAllText($"{DATASETS_DIRECTORY_PATH}/suppliers.json");

                // Console.WriteLine(ImportSuppliers(db, jsonInput));

                // Problem 02 - Import Parts
                // var jsonInput = File.ReadAllText($"{DATASETS_DIRECTORY_PATH}/parts.json");

                // Console.WriteLine(ImportParts(db, jsonInput));

                // Problem 03 - Import Cars
                // var jsonInput = File.ReadAllText($"{DATASETS_DIRECTORY_PATH}/cars.json");

                // Console.WriteLine(ImportCars(db, jsonInput));

                // Problem 04 - Import Customers
                // var jsonInput = File.ReadAllText($"{DATASETS_DIRECTORY_PATH}/customers.json");

                // Console.WriteLine(ImportCustomers(db, jsonInput));

                // Problem 05 - Import Sales
                // var jsonInput = File.ReadAllText($"{DATASETS_DIRECTORY_PATH}/sales.json");

                // Console.WriteLine(ImportSales(db, jsonInput));

                // IMPORT PARTS TO CARS
                // ImportPartCars(db);

                // Problem 06 - Export Ordered Customers
                // var json = GetOrderedCustomers(db);

                // File.WriteAllText($"{RESULTS_DIRECTORY_PATH}/ordered-customers.json", json);

                // Problem 07 - Export Cars from Make Toyota
                // var json = GetCarsFromMakeToyota(db);

                // File.WriteAllText($"{RESULTS_DIRECTORY_PATH}/toyota-cars.json", json);

                // Problem 08 - Export Local Suppliers
                // var json = GetLocalSuppliers(db);

                // File.WriteAllText($"{RESULTS_DIRECTORY_PATH}/local-suppliers.json", json);

                // Problem 09 - Export Cars with Their List of Parts
                // var json = GetCarsWithTheirListOfParts(db);

                // File.WriteAllText($"{RESULTS_DIRECTORY_PATH}/cars-and-parts.json", json);

                // Problem 10 - Export Total Sales by Customer
                //var json = GetTotalSalesByCustomer(db);

                //File.WriteAllText($"{RESULTS_DIRECTORY_PATH}/customers-total-sales.json", json);

                // Problem 11 - Export Sales with Applied Discount
                var json = GetSalesWithAppliedDiscount(db);

                File.WriteAllText($"{RESULTS_DIRECTORY_PATH}/sales-discounts.json", json);
            }
        }

        // Problem 11 - Export Sales with Applied Discount
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            using (context)
            {
                var sales = context
                    .Sales
                    .Select(s => new CustomerSaleDTO()
                    {
                        Car = new SaleDTO()
                        {
                            Make = s.Car.Make,
                            Model = s.Car.Model,
                            TravelledDistance = s.Car.TravelledDistance
                        },
                        CustomerName = s.Customer.Name,
                        Discount = s.Discount,
                        Price = s.Car.PartCars.Sum(pc => pc.Part.Price),
                        PriceWithDiscount = s.Car.PartCars.Sum(pc => pc.Part.Price) -
                                            s.Car.PartCars.Sum(pc => pc.Part.Price) * s.Discount / 100
                    })
                    .ToList();

                var json = JsonConvert.SerializeObject(sales, Formatting.Indented);

                return json;
            }
        }

        // Problem 10 - Export Total Sales by Customer
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            using (context)
            {
                var customers = context
                    .Customers
                    .ProjectTo<CustomerTotalSalesDTO>()
                    .Where(c => c.BoughtCars >= 1)
                    .OrderByDescending(c => c.SpentMoney)
                    .ThenByDescending(c => c.BoughtCars)
                    .ToList();

                var json = JsonConvert.SerializeObject(customers, Formatting.Indented);

                return json;
            }
        }

        // Problem 09 - Export Cars with Their List of Parts
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            using (context)
            {
                var cars = context
                    .Cars
                    .Select(c => new
                    {
                        car = new
                        {
                            Make = c.Make,
                            Model = c.Model,
                            TravelledDistance = c.TravelledDistance
                        },
                        parts = c.PartCars.Select(pc => new
                        {
                            Name = pc.Part.Name,
                            Price = pc.Part.Price.ToString("f2")
                        })
                            .ToList()
                    })
                    .ToList();

                var json = JsonConvert.SerializeObject(cars, Formatting.Indented);

                return json;
            }
        }

        // Problem 08 - Export Local Suppliers
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            using (context)
            {
                var suppliers = context
                    .Suppliers
                    .Where(s => !s.IsImporter)
                    .ProjectTo<SuppliersDTO>()
                    .ToList();

                var json = JsonConvert.SerializeObject(suppliers, Formatting.Indented);

                return json;
            }
        }

        // Problem 07 - Export Cars from Make Toyota
        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            using (context)
            {
                var toyotaCars = context
                    .Cars
                    .ProjectTo<CarsDTO>()
                    .OrderBy(c => c.Model)
                    .ThenByDescending(c => c.TravelledDistance)
                    .ToList();

                var json = JsonConvert.SerializeObject(toyotaCars, new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    Culture = CultureInfo.InvariantCulture,
                });

                return json;
            }
        }


        // Problem 06 - Export Ordered Customers
        public static string GetOrderedCustomers(CarDealerContext context)
        {
            using (context)
            {
                var customers = context
                    .Customers
                    .Select(c => new CustomersDTO()
                    {
                        Name = c.Name,
                        BirthDate = c.BirthDate.ToString("dd/MM/yyyy"),
                        IsYoungDriver = c.IsYoungDriver
                    })
                    .OrderBy(c => c.BirthDate)
                    .ThenBy(c => c.IsYoungDriver)
                    .ToList();

                var json = JsonConvert.SerializeObject(customers, Formatting.Indented);

                return json;
            }
        }

        // IMPORT PART TO CARS
        private static void ImportPartCars(CarDealerContext dbContext)
        {
            int carsCount = dbContext
                .Cars
                .Count();

            var partCars = new List<PartCar>();

            for (int i = 1; i <= carsCount; i++)
            {
                var partCar = new PartCar();

                partCar.CarId = i;

                partCar.PartId = new Random().Next(1, 118);

                partCars.Add(partCar);
            }

            dbContext.PartCars.AddRange(partCars);

            dbContext.SaveChanges();
        }

        // Problem 05 - Import Sales
        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            using (context)
            {
                var sales = JsonConvert.DeserializeObject<List<Sale>>(inputJson);

                context.AddRange(sales);

                context.SaveChanges();

                return $"Successfully imported {sales.Count}";
            }
        }

        // Problem 04 - Import Customers
        public static string ImportCustomers(CarDealerContext context, string
            inputJson)
        {
            using (context)
            {
                var customers = JsonConvert.DeserializeObject<List<Customer>>(inputJson);

                context.AddRange(customers);

                context.SaveChanges();

                return $"Successfully imported {customers.Count}";
            }
        }

        // Problem 03 - Import Cars
        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            using (context)
            {
                var cars = JsonConvert.DeserializeObject<List<Car>>(inputJson);

                context.AddRange(cars);

                context.SaveChanges();

                return $"Successfully imported {cars.Count}";
            }
        }

        // Problem 02 - Import Parts
        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            using (context)
            {
                var parts = JsonConvert.DeserializeObject<List<Part>>(inputJson)
                    .Where(p => Enumerable.Range(context.Suppliers
                                                        .Min(s => s.Id),
                                                 context.Suppliers
                                                        .Max(s => s.Id))
                                          .Contains(p.SupplierId))
                    .ToList();

                context.AddRange(parts);

                context.SaveChanges();

                return $"Successfully imported {parts.Count}";
            }
        }

        // Problem 01 - Import Suppliers
        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            using (context)
            {
                var suppliers = JsonConvert.DeserializeObject<List<Supplier>>(inputJson);

                context.AddRange(suppliers);

                context.SaveChanges();

                return $"Successfully imported {suppliers.Count}";
            }
        }

        // Reset Database to empty!
        private static void ResetDatabase(CarDealerContext db)
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
                cfg.AddProfile<CarDealerProfile>();
            });
        }
    }
}