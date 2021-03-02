using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Xml.Serialization;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarDealer.Data;
using CarDealer.Dtos.Export;
using CarDealer.Dtos.Import;
using CarDealer.Models;

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
                // TODO ResetDatabase(db);

                InitializeMapper();

                // TODO Problem 01 - Import Suppliers
                //var inputXml = File.ReadAllText($"{DATASETS_DIRECTORY_PATH}/suppliers.xml");

                //Console.WriteLine(ImportSuppliers(db, inputXml));

                // TODO Problem 02 - Import Parts
                //var inputXml = File.ReadAllText($"{DATASETS_DIRECTORY_PATH}/parts.xml");

                //Console.WriteLine(ImportParts(db, inputXml));

                // TODO Problem 03 - Import Cars
                //var inputXml = File.ReadAllText($"{DATASETS_DIRECTORY_PATH}/cars.xml");

                //Console.WriteLine(ImportCars(db, inputXml));

                // TODO Problem 04 - Import Customers
                //var inputXml = File.ReadAllText($"{DATASETS_DIRECTORY_PATH}/customers.xml");

                //Console.WriteLine(ImportCustomers(db, inputXml));

                // TODO Problem 05 - Import Sales
                //var inputXml = File.ReadAllText($"{DATASETS_DIRECTORY_PATH}/sales.xml");

                //Console.WriteLine(ImportSales(db, inputXml));

                // TODO Problem 06 - Export Cars With Distance
                //var result = GetCarsWithDistance(db);

                //File.WriteAllText($"{RESULTS_DIRECTORY_PATH}/cars.xml", result);

                // TODO P07 - Export Cars from make BMW
                //var result = GetCarsFromMakeBmw(db);

                //File.WriteAllText($"{RESULTS_DIRECTORY_PATH}/bmw-cars.xml", result);

                // TODO Problem 08 - Export Local Suppliers
                //var result = GetLocalSuppliers(db);

                //File.WriteAllText($"{RESULTS_DIRECTORY_PATH}/local-suppliers.xml", result);

                // TODO Problem 09 - Cars with Their List of Parts
                //var result = GetCarsWithTheirListOfParts(db);

                //File.WriteAllText($"{RESULTS_DIRECTORY_PATH}/cars-and-parts.xml", result);

                // TODO Problem 10 - Total Sales by Customer
                //var result = GetTotalSalesByCustomer(db);

                //File.WriteAllText($"{RESULTS_DIRECTORY_PATH}/customers-total-sales.xml", result);

                // TODO Problem 11 - Sales with Applied Discount
                var result = GetSalesWithAppliedDiscount(db);

                File.WriteAllText($"{RESULTS_DIRECTORY_PATH}/sales-discounts.xml", result);
            }
        }

        // TODO Problem 11 - Sales with Applied Discount
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var sb = new StringBuilder();

            using (context)
            {
                var sales = context
                    .Sales
                    .Select(s => new ExportSalesWithAppliedDiscount()
                    {
                        Discount = s.Discount,
                        CustomerName = s.Customer.Name,
                        Price = s.Car.PartCars.Sum(pc => pc.Part.Price),
                        PriceWithDiscount = s.Car.PartCars.Sum(pc => pc.Part.Price) -
                                            s.Car.PartCars.Sum(pc => pc.Part.Price) * s.Discount / 100,
                        Car = new ExportCarForDiscountDTO()
                        {
                            Make = s.Car.Make,
                            Model = s.Car.Model,
                            TravelledDistance = s.Car.TravelledDistance
                        },
                    })
                    .ToList();

                var xmlSerializer = new XmlSerializer(typeof(List<ExportSalesWithAppliedDiscount>),
                    new XmlRootAttribute("sales"));

                var namespaces = new XmlSerializerNamespaces();

                namespaces.Add("", "");

                var writer = new StringWriter(sb);

                using (writer)
                {
                    xmlSerializer.Serialize(writer, sales, namespaces);
                }
            }

            return sb.ToString().Trim();
        }

        // TODO Problem 10 - Total Sales by Customer
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var sb = new StringBuilder();

            using (context)
            {
                var customers = context
                    .Customers
                    .ProjectTo<ExportTotalSalesByCustomer>()
                    .Where(c => c.BoughtCars >= 1)
                    .OrderByDescending(c => c.SpentMoney)
                    .ToList();

                var xmlSerializer = new XmlSerializer(typeof(List<ExportTotalSalesByCustomer>),
                    new XmlRootAttribute("customers"));

                var namespaces = new XmlSerializerNamespaces();

                namespaces.Add("", "");

                var writer = new StringWriter(sb);

                using (writer)
                {
                    xmlSerializer.Serialize(writer, customers, namespaces);
                }
            }

            return sb.ToString().Trim();
        }

        // TODO Problem 09 - Cars with Their List of Parts
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var sb = new StringBuilder();

            using (context)
            {
                var cars = context
                    .Cars
                    .ProjectTo<ExportCarDTO>()
                    .OrderByDescending(c => c.TravelledDistance)
                    .ThenBy(c => c.Model)
                    .Take(5)
                    .ToList();

                var xmlSerializer = new XmlSerializer(typeof(List<ExportCarDTO>), new XmlRootAttribute("cars"));

                var namespaces = new XmlSerializerNamespaces();

                namespaces.Add("", "");

                var writer = new StringWriter(sb);

                using (writer)
                {
                    xmlSerializer.Serialize(writer, cars, namespaces);
                }
            }

            return sb.ToString().Trim();
        }

        // TODO Problem 08 - Export Local Suppliers
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var sb = new StringBuilder();

            using (context)
            {
                var suppliers = context
                    .Suppliers
                    .Where(s => s.IsImporter == false)
                    .ProjectTo<ExportLocalSupplierDTO>()
                    .ToList();

                var xmlSerializer = new XmlSerializer(typeof(List<ExportLocalSupplierDTO>),
                    new XmlRootAttribute("suppliers"));

                var namespaces = new XmlSerializerNamespaces();
                namespaces.Add("", "");

                var writer = new StringWriter(sb);

                using (writer)
                {
                    xmlSerializer.Serialize(writer, suppliers, namespaces);
                }
            }

            return sb.ToString().Trim();
        }

        // TODO Problem 07 - Export Cars from make BMW
        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            var sb = new StringBuilder();

            using (context)
            {
                var cars = context
                    .Cars
                    .Where(c => c.Make == "BMW")
                    .ProjectTo<ExportGetCarsFromMakeBmwDTO>()
                    .OrderBy(c => c.Model)
                    .ThenBy(c => c.TravelledDistance)
                    .ToList();

                var xmlSerializer = new XmlSerializer(typeof(List<ExportGetCarsFromMakeBmwDTO>), new XmlRootAttribute("cars"));

                var namespaces = new XmlSerializerNamespaces();

                namespaces.Add("", "");

                var writer = new StringWriter(sb);

                using (writer)
                {
                    xmlSerializer.Serialize(writer, cars, namespaces);
                }
            }

            return sb.ToString().Trim();
        }

        // TODO Problem 06 - Export Cars With Distance
        public static string GetCarsWithDistance(CarDealerContext context)
        {
            var sb = new StringBuilder();

            using (context)
            {
                var cars = context
                    .Cars
                    .Where(c => c.TravelledDistance > 2_000_000)
                    .ProjectTo<ExportCarsWithDistanceDTO>()
                    .OrderBy(c => c.Make)
                    .ThenBy(c => c.Model)
                    .Take(10)
                    .ToList();

                var xmlSerializer = new XmlSerializer(typeof(List<ExportCarsWithDistanceDTO>), new XmlRootAttribute("cars"));

                var namespaces = new XmlSerializerNamespaces();

                namespaces.Add("", "");

                var writer = new StringWriter(sb);

                using (writer)
                {
                    xmlSerializer.Serialize(writer, cars, namespaces);
                }
            }

            return sb.ToString().Trim();
        }

        // TODO Problem 05 - Import Sales
        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            using (context)
            {
                var xmlSerializer = new XmlSerializer(typeof(List<ImportSalesDTO>), new XmlRootAttribute("Sales"));

                var reader = new StringReader(inputXml);

                using (reader)
                {
                    var saleDtos = (List<ImportSalesDTO>)xmlSerializer.Deserialize(reader);

                    var sales = Mapper.Map<List<Sale>>(saleDtos)
                        .Where(p => context.Cars.Any(c => c.Id == p.CarId)).ToList();

                    context.AddRange(sales);

                    context.SaveChanges();

                    return $"Successfully imported {sales.Count}";
                }
            }
        }

        // TODO Problem 04 - Import Customers
        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            using (context)
            {
                var xmlSerializer =
                    new XmlSerializer(typeof(List<ImportCustomersDTO>), new XmlRootAttribute("Customers"));

                var reader = new StringReader(inputXml);

                using (reader)
                {
                    var customerDtos = (List<ImportCustomersDTO>)xmlSerializer.Deserialize(reader);

                    var customers = Mapper.Map<List<Customer>>(customerDtos);

                    context.AddRange(customers);

                    context.SaveChanges();

                    return $"Successfully imported {customers.Count}";
                }
            }
        }

        // TODO Problem 03 - Import Cars
        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            using (context)
            {
                var xmlSerializer = new XmlSerializer(typeof(List<ImportCarDTO>), new XmlRootAttribute("Cars"));

                var reader = new StringReader(inputXml);

                using (reader)
                {
                    var carDtos = (List<ImportCarDTO>)xmlSerializer.Deserialize(reader);

                    var cars = new List<Car>();
                    var partCars = new List<PartCar>();

                    foreach (var carDto in carDtos)
                    {
                        var car = new Car()
                        {
                            Make = carDto.Make,
                            Model = carDto.Model,
                            TravelledDistance = carDto.TravelledDistance,
                        };

                        var parts = carDto
                            .Parts
                            .Where(pdto => context
                                            .Parts
                                            .Any(p => p.Id == pdto.Id))
                            .Select(p => p.Id)
                            .Distinct();

                        foreach (var part in parts)
                        {
                            var partCar = new PartCar()
                            {
                                PartId = part,
                                Car = car
                            };

                            partCars.Add(partCar);
                        }

                        cars.Add(car);
                    }

                    context.AddRange(cars);

                    context.AddRange(partCars);

                    context.SaveChanges();

                    return $"Successfully imported {cars.Count}";
                }
            }
        }

        // TODO Problem 02 - Import Parts
        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            using (context)
            {
                var xmlSerializer = new XmlSerializer(typeof(List<ImportPartDTO>), new XmlRootAttribute("Parts"));

                var reader = new StringReader(inputXml);

                using (reader)
                {
                    var partDtos = (List<ImportPartDTO>)xmlSerializer.Deserialize(reader);

                    var parts = Mapper.Map<List<Part>>(partDtos).Where(p => Enumerable.Range(context.Suppliers.Min(s => s.Id), context.Suppliers.Max(s => s.Id)).Contains(p.SupplierId)).ToList();

                    context.AddRange(parts);

                    context.SaveChanges();

                    return $"Successfully imported {parts.Count}";
                }
            }
        }

        // TODO Problem 01 - Import Suppliers
        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            using (context)
            {
                var xmlSerializer = new XmlSerializer(typeof(List<ImportSupplierDTO>), new XmlRootAttribute("Suppliers"));

                var reader = new StringReader(inputXml);

                using (reader)
                {
                    var supplierDtos = (List<ImportSupplierDTO>)xmlSerializer.Deserialize(reader);

                    var suppliers = Mapper.Map<List<Supplier>>(supplierDtos);

                    context.AddRange(suppliers);

                    context.SaveChanges();

                    return $"Successfully imported {suppliers.Count}";
                }
            }
        }

        // TODO Initializing the Mapper
        private static void InitializeMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<CarDealerProfile>();
            });
        }

        // TODO Reset Database to empty!
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
    }
}