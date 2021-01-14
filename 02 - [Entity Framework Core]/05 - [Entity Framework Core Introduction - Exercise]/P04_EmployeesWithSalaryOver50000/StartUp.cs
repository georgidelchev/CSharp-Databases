using System;
using System.Linq;
using System.Text;
using EntityFrameworkIntroductionExercise.Data;

namespace P04_EmployeesWithSalaryOver50000
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var db = new SoftUniContext();

            using (db)
            {
                Console.WriteLine(GetEmployeesWithSalaryOver50000(db));
            }
        }

        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            var sb = new StringBuilder();

            using (context)
            {
                var employeesData = context.Employees
                    .Where(e => e.Salary > 50000)
                    .Select(e => new
                    {
                        FirstName = e.FirstName,
                        Salary = e.Salary
                    })
                    .OrderBy(e => e.FirstName)
                    .ToList();

                foreach (var employee in employeesData)
                {
                    sb.AppendLine($"{employee.FirstName} - " +
                                  $"{employee.Salary:f2}");
                }
            }

            return sb.ToString().Trim();
        }
    }
}
