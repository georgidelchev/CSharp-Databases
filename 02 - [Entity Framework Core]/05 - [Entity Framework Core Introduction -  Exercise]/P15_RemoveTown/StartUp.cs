using System;
using System.Linq;
using System.Text;
using EntityFrameworkIntroductionExercise.Data;

namespace P15_RemoveTown
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var db = new SoftUniContext();

            using (db)
            {
                Console.WriteLine(RemoveTown(db));
            }
        }

        public static string RemoveTown(SoftUniContext context)
        {
            using (context)
            {
                var townToDelete = context.Towns
                    .FirstOrDefault(t => t.Name == "Seattle");

                var addressesToDelete = context.Addresses
                    .Where(a => a.TownId == townToDelete.TownId);

                var addressesDeletedCount = addressesToDelete.Count();

                var employeesAddressesToReplace = context.Employees
                    .Where(e => addressesToDelete.Any(a => a.AddressId == e.AddressId));

                foreach (var employee in employeesAddressesToReplace)
                {
                    employee.AddressId = null;
                }

                foreach (var address in addressesToDelete)
                {
                    context.Addresses.Remove(address);
                }

                context.Towns.Remove(townToDelete);

                context.SaveChanges();

                return addressesDeletedCount + " addresses in Seattle were deleted";
            }
        }
    }
}
