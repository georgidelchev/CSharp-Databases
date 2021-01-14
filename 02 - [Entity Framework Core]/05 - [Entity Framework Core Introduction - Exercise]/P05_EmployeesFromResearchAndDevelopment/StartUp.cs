using System;
using System.Linq;
using System.Text;
using EntityFrameworkIntroductionExercise.Data;

namespace P05_EmployeesFromResearchAndDevelopment
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var db = new SoftUniContext();

            using (db)
            {
                Console.WriteLine(GetEmployeesFromResearchAndDevelopment(db));
            }
        }

        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            var sb = new StringBuilder();

            using (context)
            {
                var employeesData = context.Employees
                    .Where(e => e.Department.Name == "Research and Development")
                    .Select(e => new
                    {
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        DepartmentName = e.Department.Name,
                        Salary = e.Salary
                    })
                    .OrderBy(e => e.Salary)
                    .ThenByDescending(e => e.FirstName)
                    .ToList();

                foreach (var employee in employeesData)
                {
                    sb.AppendLine(
                        $"{employee.FirstName} " +
                        $"{employee.LastName} " +
                        $"{employee.DepartmentName} " +
                        $"{employee.Salary:f2}");
                }
            }

            return sb.ToString().Trim();
        }
    }
}
