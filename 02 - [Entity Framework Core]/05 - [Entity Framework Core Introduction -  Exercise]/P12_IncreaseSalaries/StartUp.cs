using System;
using System.Linq;
using System.Text;
using EntityFrameworkIntroductionExercise.Data;

namespace P12_IncreaseSalaries
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var db = new SoftUniContext();

            using (db)
            {
                Console.WriteLine(IncreaseSalaries(db));
            }
        }

        public static string IncreaseSalaries(SoftUniContext context)
        {
            var sb = new StringBuilder();

            using (context)
            {
                var departments = new[]
                {
                    "Engineering",
                    "Tool Design",
                    "Marketing",
                    "Information Services"
                };

                var employeesData = context.Employees
                    .Where(e => departments.Contains(e.Department.Name))
                    .OrderBy(e => e.FirstName)
                    .ThenBy(e => e.LastName)
                    .ToList();

                foreach (var employee in employeesData)
                {
                    employee.Salary *= 1.12m;
                }

                context.SaveChanges();

                foreach (var employee in employeesData)
                {
                    sb.AppendLine($"{employee.FirstName} {employee.LastName} (${employee.Salary:f2})");
                }
            }

            return sb.ToString().Trim();
        }
    }
}
