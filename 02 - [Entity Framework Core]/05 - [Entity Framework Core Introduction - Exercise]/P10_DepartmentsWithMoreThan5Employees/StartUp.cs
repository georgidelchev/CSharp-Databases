using System;
using System.Linq;
using System.Text;
using EntityFrameworkIntroductionExercise.Data;

namespace P10_DepartmentsWithMoreThan5Employees
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var db = new SoftUniContext();

            using (db)
            {
                Console.WriteLine(DepartmentsWithMoreThan5Employees(db));
            }
        }

        public static string DepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            var sb = new StringBuilder();

            using (context)
            {
                var departments = context.Departments
                    .Where(e => e.Employees.Count > 5)
                    .OrderBy(e => e.Employees.Count)
                    .ThenBy(d => d.Name)
                    .Select(d => new
                    {
                        Name = d.Name,
                        ManagerFirstName = d.Manager.FirstName,
                        ManagerLastName = d.Manager.LastName,
                        Employees = d.Employees.Select(e => new
                        {
                            FirstName = e.FirstName,
                            LastName = e.LastName,
                            JobTitle = e.JobTitle
                        })
                                              .OrderBy(e => e.FirstName)
                                              .ThenBy(e => e.LastName)
                                              .ToList()
                    })
                    .ToList();

                foreach (var department in departments)
                {
                    sb.AppendLine($"{department.Name} - " +
                                  $"{department.ManagerFirstName} " +
                                  $"{department.ManagerLastName}");

                    foreach (var employee in department.Employees)
                    {
                        sb.AppendLine($"{employee.FirstName} " +
                                      $"{employee.LastName} - " +
                                      $"{employee.JobTitle}");
                    }
                }
            }

            return sb.ToString().Trim();
        }
    }
}
