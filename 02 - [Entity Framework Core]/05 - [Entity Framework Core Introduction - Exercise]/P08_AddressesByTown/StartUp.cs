using System;
using System.Linq;
using System.Text;
using EntityFrameworkIntroductionExercise.Data;

namespace P08_AddressesByTown
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var db = new SoftUniContext();

            using (db)
            {
                Console.WriteLine(GetAddressesByTown(db));
            }
        }

        public static string GetAddressesByTown(SoftUniContext context)
        {
            var sb = new StringBuilder();

            using (context)
            {
                var addressesInfo = context.Addresses
                    .Select(a => new
                    {
                        EmployeesCount = a.Employees.Count,
                        TownName = a.Town.Name,
                        AddressText = a.AddressText
                    })
                    .OrderByDescending(e => e.EmployeesCount)
                    .ThenBy(e => e.TownName)
                    .ThenBy(a => a.AddressText)
                    .Take(10)
                    .ToList();

                foreach (var address in addressesInfo)
                {
                    sb.AppendLine($"{address.AddressText} - " +
                                  $"{address.TownName} - " +
                                  $"{address.EmployeesCount} employees");
                }
            }

            return sb.ToString().Trim();
        }
    }
}
