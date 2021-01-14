using System;
using System.Linq;
using System.Text;
using EntityFrameworkIntroductionExercise.Data;

namespace P13_FindEmployeesByFirstNameStartingWithSa
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var db = new SoftUniContext();

            using (db)
            {
                Console.WriteLine(FindEmployeesByFirstNameStartingWithSa(db));
            }
        }

        public static string FindEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            var sb = new StringBuilder();

            using (context)
            {
                var employees = context.Employees
                    .Where(e => e.FirstName.StartsWith("Sa"))
                    .Select(e => new
                    {
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        JobTitle = e.JobTitle,
                        Salary = e.Salary
                    })
                    .OrderBy(e => e.FirstName)
                    .ThenBy(e => e.LastName)
                    .ToList();

                foreach (var employee in employees)
                {
                    sb.AppendLine($"{employee.FirstName} - " +
                                  $"{employee.LastName} - " +
                                  $"{employee.JobTitle} - " +
                                  $"(${employee.Salary:f2})");
                }
            }

            return sb.ToString().Trim();
        }
    }
}
