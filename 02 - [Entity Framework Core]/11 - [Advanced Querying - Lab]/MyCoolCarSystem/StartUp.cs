using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyCoolCarSystem.Data;
using MyCoolCarSystem.Data.Models;
using MyCoolCarSystem.Results;

namespace MyCoolCarSystem
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var db = new CarDbContext();

            using (db)
            {
                db.Database.Migrate();

                // AddMakesToDb(db);

                // AddModelsToOpel(db);

                // AddCarsToInsignia(db);

                // AddCarToCustomer(db);

                // AddAddressToCustomer(db);

                // GetPurchase(db);

                var result1 = db
                    .Cars
                    .FromSqlInterpolated($"SELECT * FROM Cars WHERE Price > 5000")
                    .ToList();

                db.Cars
                    .Where(c => c.Price > 5000)
                    .Select(c => new ResultModel
                    {
                        FullName = c.Model.Make.Name
                    })
                    .ToList();

                var query = EF.CompileQuery<CarDbContext, IEnumerable<ResultModel>> (db => db
                    .Cars
                    .Where(c => c.Price > 5000)
                    .Select(c => new ResultModel
                    {
                        FullName = c.Model.Make.Name
                    }));

                var result2 = query(db);

                db.SaveChanges();
            }
        }

        private static void GetPurchase(CarDbContext db)
        {
            var purchases = db.CarPurchases
                   .Select(p => new PurchaseResultModel()
                   {
                       Price = p.Price,
                       PurchaseDate = p.PurchaseDate,
                       Customer = new CustomerResultModel()
                       {
                           Name = p.Customer.FirstName + " " + p.Customer.LastName,
                           Town = p.Customer.Address.Town
                       },

                       Car = new CarResultModel
                       {
                           Make = p.Car.Model.Make.Name,
                           Model = p.Car.Model.Name,
                           Vin = p.Car.Vin,
                       }
                   })
                   .ToList();
        }

        private static void AddAddressToCustomer(CarDbContext db)
        {
            var customer = db.Customers.FirstOrDefault();

            customer.Address = new Address
            {
                Text = "Tintiava 15",
                Town = "Sofia"
            };
        }

        private static void AddCarToCustomer(CarDbContext db)
        {
            var car = db.Cars.FirstOrDefault();

            var customer = new Customer
            {
                FirstName = "Ivan",
                LastName = "Ivanov",
                Age = 56,
            };

            customer.Purchases.Add(new CarPurchase
            {
                Car = car,
                PurchaseDate = DateTime.Now,
                Price = car.Price * 0.9m
            });

            db.Customers.Add(customer);
        }

        private static void AddCarsToInsignia(CarDbContext db)
        {
            var insigniaModel = db
                                .Models
                                .FirstOrDefault(m => m.Name == "Insignia");

            insigniaModel.Cars.Add(new Car
            {
                Name = "Black Panther",
                Color = "Black",
                Price = 20000,
                ProductionDate = DateTime.UtcNow,
                Vin = "SomeVinNumber1"
            });

            insigniaModel.Cars.Add(new Car
            {
                Name = "White Panther",
                Color = "White",
                Price = 10000,
                ProductionDate = DateTime.UtcNow,
                Vin = "SomeVinNumber2"
            });

            insigniaModel.Cars.Add(new Car
            {
                Name = "Orange Panther",
                Color = "Orange",
                Price = 220000,
                ProductionDate = DateTime.UtcNow,
                Vin = "SomeVinNumber3"
            });
        }

        private static void AddModelsToOpel(CarDbContext db)
        {
            var opelMake = db
                    .Makes
                    .FirstOrDefault(m => m.Name == "Opel");

            opelMake.Models.Add(new Model
            {
                Name = "Astra",
                Year = 2017,
                Modification = "2.2D",

            });

            opelMake.Models.Add(new Model
            {
                Name = "Insignia",
                Year = 2019,
                Modification = "1.9TDI",

            });
        }

        private static void AddMakesToDb(CarDbContext db)
        {
            db.Makes.Add(new Make
            {
                Name = "Mercedes"
            });

            db.Makes.Add(new Make
            {
                Name = "Audi"
            });

            db.Makes.Add(new Make
            {
                Name = "BMW"
            });

            db.Makes.Add(new Make
            {
                Name = "Opel"
            });

            db.Makes.Add(new Make
            {
                Name = "Peugeot"
            });
        }
    }
}
