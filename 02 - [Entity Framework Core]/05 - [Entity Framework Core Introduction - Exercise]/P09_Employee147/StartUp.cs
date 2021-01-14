using System;
using System.Linq;
using System.Text;
using EntityFrameworkIntroductionExercise.Data;

namespace P09_Employee147
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var db = new SoftUniContext();

            using (db)
            {
                Console.WriteLine(GetEmployee147(db));
            }
        }

        public static string GetEmployee147(SoftUniContext context)
        {
            var sb = new StringBuilder();

            using (context)
            {
                var employeeData = context.Employees
                    .Select(e => new
                    {
                        EmployeeId = e.EmployeeId,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        JobTitle = e.JobTitle,
                        Projects = e.EmployeesProjects.Select(ep => ep.Project.Name)
                                                      .OrderBy(ep => ep)
                                                      .ToList()
                    })
                    .SingleOrDefault(e => e.EmployeeId == 147);

                sb.AppendLine($"{employeeData.FirstName} " +
                              $"{employeeData.LastName} - " +
                              $"{employeeData.JobTitle}");

                foreach (var project in employeeData.Projects)
                {
                    sb.AppendLine($"{project}");
                }
            }

            return sb.ToString().Trim();
        }
    }
}
