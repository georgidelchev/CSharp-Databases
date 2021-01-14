using System;
using System.Text;
using System.Linq;
using EntityFrameworkIntroductionExercise.Data;

namespace P03_EmployeesFullInformation
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var db = new SoftUniContext();

            using (db)
            {
                Console.WriteLine(GetEmployeesFullInformation(db));
            }
        }

        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            var sb = new StringBuilder();

            using (context)
            {
                var employeesData = context.Employees
                    .Select(e => new
                    {
                        EmployeeId = e.EmployeeId,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        MiddleName = e.MiddleName,
                        JobTitle = e.JobTitle,
                        Salary = e.Salary
                    })
                    .OrderBy(e => e.EmployeeId)
                    .ToList();

                foreach (var employee in employeesData)
                {
                    sb.AppendLine(
                        $"{employee.FirstName} " +
                        $"{employee.LastName} " +
                        $"{employee.MiddleName} " +
                        $"{employee.JobTitle} " +
                        $"{employee.Salary:f2}");
                }
            }

            return sb.ToString().Trim();
        }
    }
}
