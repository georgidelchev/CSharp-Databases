using System;
using System.Linq;
using EntityFrameworkPractice.Data;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkPractice
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var db1 = new SoftUniContext();

            using (db1)
            {
                var towns1 = db1.Towns.ToList();

                //var town = new Town()
                //{
                //     Name = "Stara Zagora"
                //};

                //db.Towns.Add(town);

                //db.SaveChanges();

                Console.WriteLine(towns1.Count + " towns");

                foreach (var town in towns1)
                {
                    Console.WriteLine(town.Name);
                }

                Console.WriteLine();

                var towns2 = db1.Towns
                    .Include(t => t.Addresses)
                    .ToList();

                foreach (var town in towns2)
                {
                    Console.WriteLine(town.Name + " - ");

                    foreach (var address in town.Addresses)
                    {
                        Console.WriteLine("-- " + address.AddressText);
                    }

                    Console.WriteLine();
                }

                Console.WriteLine();

                var towns3 = db1.Towns
                    .Where(t => t.Name.StartsWith("P"))
                    .Where(t => t.TownId > 3)
                    .ToList();

                foreach (var town in towns3)
                {
                    Console.WriteLine(town.TownId + " " + town.Name);
                }

                Console.WriteLine();

                var town4 = db1.Towns
                    .Where(t => t.Name.StartsWith("P"))
                    .Select(t => new
                    {
                        t.Name,
                        Addresses = t.Addresses.Select(a => a.AddressText)
                    })
                    .ToList();

                foreach (var town in town4)
                {
                    Console.WriteLine(town.Name);

                    foreach (var address in town.Addresses)
                    {
                        Console.WriteLine("-- " + address);
                    }
                }

                Console.WriteLine();
            }

            var db2 = new SoftUniContext();

            using (db2)
            {
                var employees = db2.Employees
                    .Select(e => new
                    {
                        e.FirstName,
                        e.LastName,
                        Department = e.Department.Name
                    })
                    .ToList();

                foreach (var employee in employees)
                {
                    Console.WriteLine($"{employee.FirstName} {employee.LastName} -> {employee.Department}");
                }
            }

            var db3 = new SoftUniContext();

            using (db3)
            {
                // Add

                //var town = new Town()
                //{
                //    Name = "New Town"
                //};

                //town.Addresses.Add(new Address()
                //{
                //    AddressText = "SomeAddress"
                //});

                //db3.Add(town);

                //db3.SaveChanges();



                // Update

                //var town2 = new Town
                //{
                //    TownId = 37,
                //    Name = "New"
                //};

                //db3.Update(town2);



                // Delete

                //var town = db3.Towns
                //    .Select(t => new
                //    {
                //        t.TownId,
                //        Addresses = t.Addresses.Select(a => a.AddressId)
                //    })
                //    .FirstOrDefault(t => t.TownId == 37);

                //foreach (var address in town.Addresses)
                //{
                //    db3.Addresses.Remove(new Address
                //    {
                //        AddressId = address 
                //    });
                //}

                //db3.Towns.Remove(new Town
                //{
                //    TownId = town.TownId
                //});

                Console.WriteLine();

                var dep = db3.Departments
                    .Where(d => d.DepartmentId == 3)
                    .Select(d => new
                    {
                        d.DepartmentId,
                        d.Name
                    })
                    .FirstOrDefault();

                Console.WriteLine(dep.Name);

                Console.WriteLine();

                var result = db3.Towns
                    .Select(t => new
                    {
                        t.Name,
                        Addresses = t.Addresses.Select(a => a.AddressText)
                    })
                    .ToList();

                db3.Towns.Join(db3.Addresses, t => t.TownId, a => a.TownId, (t, a) => new
                {
                    Name = t.Name,
                    Address = a.AddressText
                })
                    .ToList();

                db3.SaveChanges();
            }
        }
    }
}
