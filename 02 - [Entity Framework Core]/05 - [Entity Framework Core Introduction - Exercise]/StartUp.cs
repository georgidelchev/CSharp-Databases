using System;
using System.Globalization;
using System.Linq;
using System.Text;
using SoftUni.Data;
using SoftUni.Models;

namespace SoftUni
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var db = new SoftUniContext();

            using (db)
            {
                // P15
                //Console.WriteLine(RemoveTown(db));

                // P14
                //Console.WriteLine(DeleteProjectById(db));

                // P13
                // Console.WriteLine(FindEmployeesByFirstNameStartingWithSa(db));

                // P12
                // Console.WriteLine(IncreaseSalaries(db));

                // P11
                // Console.WriteLine(GetLatestProjects(db));

                // P10
                // Console.WriteLine(DepartmentsWithMoreThan5Employees(db));

                // P09
                // Console.WriteLine(GetEmployee147(db));

                // P08
                // Console.WriteLine(GetAddressesByTown(db));

                // P07
                // Console.WriteLine(GetEmployeesInPeriod(db));

                // P06
                // Console.WriteLine(AddNewAddressToEmployee(db));

                // P05
                // Console.WriteLine(GetEmployeesFromResearchAndDevelopment(db));

                // P04
                // Console.WriteLine(GetEmployeesWithSalaryOver50000(db));

                // P03
                Console.WriteLine(GetEmployeesFullInformation(db));
            }
        }

        /* ---------------------- P15 ---------------------- */

        public static string RemoveTown(SoftUniContext context)
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


        /* ---------------------- P14 ---------------------- */

        public static string DeleteProjectById(SoftUniContext context)
        {
            var sb = new StringBuilder();

            var projectToDelete = context.Projects
                .FirstOrDefault(p => p.ProjectId == 2);

            var employeeProjectsToDelete = context.EmployeesProjects
                .Where(ep => ep.ProjectId == 2)
                .ToList();

            foreach (var employeeProject in employeeProjectsToDelete)
            {
                context.EmployeesProjects.Remove(employeeProject);
            }

            context.Projects.Remove(projectToDelete);

            context.SaveChanges();

            var projects = context.Projects
                .Select(p => p.Name)
                .Take(10)
                .ToList();

            foreach (var project in projects)
            {
                sb.AppendLine($"{project}");
            }

            return sb.ToString().Trim();
        }

        /* ---------------------- P13 ---------------------- */

        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            var sb = new StringBuilder();

            if (context.Employees.Any(e => e.FirstName == "Svetlin"))
            {
                string pattern = "SA";
                var employeesByNamePattern = context.Employees
                    .Where(employee => employee.FirstName.StartsWith(pattern));

                foreach (var employeeByPattern in employeesByNamePattern)
                {
                    sb.AppendLine($"{employeeByPattern.FirstName} {employeeByPattern.LastName} " +
                                       $"- {employeeByPattern.JobTitle} - (${employeeByPattern.Salary})");
                }
            }
            else
            {
                var employeesByNamePattern = context.Employees.Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.JobTitle,
                    e.Salary,
                })
                    .Where(e => e.FirstName.StartsWith("Sa"))
                    .OrderBy(e => e.FirstName)
                    .ThenBy(e => e.LastName)
                    .ToList();

                foreach (var employee in employeesByNamePattern)
                {
                    sb.AppendLine($"{employee.FirstName} {employee.LastName} " +
                                       $"- {employee.JobTitle} - (${employee.Salary:F2})");
                }
            }

            return sb.ToString().TrimEnd();
        }

        /* ---------------------- P12 ---------------------- */

        public static string IncreaseSalaries(SoftUniContext context)
        {
            var sb = new StringBuilder();

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


            return sb.ToString().Trim();
        }

        /* ---------------------- P11 ---------------------- */

        public static string GetLatestProjects(SoftUniContext context)
        {
            var sb = new StringBuilder();

            var projects = context.Projects
                .OrderByDescending(p => p.StartDate)
                .Take(10)
                .Select(p => new
                {
                    Name = p.Name,
                    Desctiption = p.Description,
                    StartDate = p.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)
                })
                .OrderBy(project => project.Name)
                .ToList();

            foreach (var project in projects)
            {
                sb.AppendLine(
                    $"{project.Name}" +
                    $"{Environment.NewLine}" +
                    $"{project.Desctiption}" +
                    $"{Environment.NewLine}" +
                    $"{project.StartDate}");
            }

            return sb.ToString().Trim();
        }

        /* ---------------------- P10 ---------------------- */

        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            var sb = new StringBuilder();

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

            return sb.ToString().Trim();
        }

        /* ---------------------- P09 ---------------------- */

        public static string GetEmployee147(SoftUniContext context)
        {
            var sb = new StringBuilder();

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


            return sb.ToString().Trim();
        }

        /* ---------------------- P08 ---------------------- */

        public static string GetAddressesByTown(SoftUniContext context)
        {
            var sb = new StringBuilder();

            var addressesInfo = context.Addresses
                .OrderByDescending(e => e.Employees.Count)
                .ThenBy(e => e.Town.Name)
                .ThenBy(a => a.AddressText)
                .Take(10)
                .Select(a => new
                {
                    EmployeesCount = a.Employees.Count,
                    TownName = a.Town.Name,
                    AddressText = a.AddressText
                })
                .ToList();

            foreach (var address in addressesInfo)
            {
                sb.AppendLine($"{address.AddressText}, " +
                              $"{address.TownName} - " +
                              $"{address.EmployeesCount} employees");
            }

            return sb.ToString().Trim();
        }

        /* ---------------------- P07 ---------------------- */

        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            var sb = new StringBuilder();

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
                sb.AppendLine($"{employee.FirstName} " +
                                  $"{employee.LastName} - " +
                                  $"Manager: {employee.ManagerFirstName} " +
                                           $"{employee.ManagerLastName}");

                foreach (var project in employee.Project)
                {
                    sb.AppendLine($"--{project.Name} - " +
                                      $"{project.StartDate} - " +
                                      $"{project.EndDate}");
                }
            }

            return sb.ToString().Trim();
        }

        /* ---------------------- P06 ---------------------- */

        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            var sb = new StringBuilder();

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

            return sb.ToString().Trim();
        }

        /* ---------------------- P05 ---------------------- */

        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            var sb = new StringBuilder();

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
                sb.AppendLine($"{employee.FirstName} {employee.LastName} " +
                           $"from {employee.DepartmentName} - ${employee.Salary:F2}");
            }

            return sb.ToString().Trim();
        }

        /* ---------------------- P04 ---------------------- */

        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            var sb = new StringBuilder();

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

            return sb.ToString().Trim();
        }

        /* ---------------------- P03 ---------------------- */

        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            var sb = new StringBuilder();

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

            return sb.ToString().Trim();
        }
    }
}
