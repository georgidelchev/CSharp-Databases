using System;
using System.Globalization;
using System.Linq;
using System.Text;
using EntityFrameworkIntroductionExercise.Data;

namespace P07_EmployeesAndProjects
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var db = new SoftUniContext();

            using (db)
            {
                Console.WriteLine(GetEmployeesInPeriod(db));
            }
        }

        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            var sb = new StringBuilder();

            using (context)
            {
                var employeesAndProjectsData = context.Employees
                    .Where(e => e.EmployeesProjects
                                 .Any(ep => ep.Project.StartDate.Year >= 2001 && 
                                            ep.Project.StartDate.Year <= 2003))
                    .Select(e => new
                    {
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        ManagerFirstName = e.Manager.FirstName,
                        ManagerLastName = e.Manager.LastName,
                        Project = e.EmployeesProjects.Select(ep => new
                        {
                            Name = ep.Project.Name,
                            StartDate = ep.Project
                                          .StartDate
                                          .ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture),
                            EndDate = ep.Project
                                        .EndDate.HasValue ?
                                              ep.Project.EndDate
                                                        .Value
                                                        .ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture) :
                                              "not finished"
                        })
                    })
                    .Take(10)
                    .ToList();

                foreach (var employee in employeesAndProjectsData)
                {
                    Console.WriteLine($"{employee.FirstName} " +
                                      $"{employee.LastName} - " +
                                      $"Manager: {employee.ManagerFirstName} " +
                                               $"{employee.ManagerLastName}");

                    foreach (var project in employee.Project)
                    {
                        Console.WriteLine($"--{project.Name} - " +
                                          $"{project.StartDate} - " +
                                          $"{project.EndDate}");
                    }
                }
            }

            return sb.ToString().Trim();
        }
    }
}
