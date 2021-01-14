using System;
using System.Linq;
using System.Text;
using EntityFrameworkIntroductionExercise.Data;

namespace P06_AddingANewAddressAndUpdatingEmployee
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var db = new SoftUniContext();

            using (db)
            {
                Console.WriteLine(AddNewAddressToEmployee(db));
            }
        }

        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            var sb = new StringBuilder();

            using (context)
            {
                var newAddress = new Address
                {
                    AddressText = "Vitoshka 15",
                    TownId = 4
                };

                var employee = context.Employees
                    .FirstOrDefault(e => e.LastName == "Nakov");

                employee.Address = newAddress;

                context.SaveChanges();

                var addresses = context.Employees
                    .OrderByDescending(e => e.AddressId)
                    .Take(10)
                    .Select(e => e.Address.AddressText)
                    .ToList();

                foreach (var address in addresses)
                {
                    sb.AppendLine(address);
                }
            }

            return sb.ToString().Trim();
        }
    }
}
