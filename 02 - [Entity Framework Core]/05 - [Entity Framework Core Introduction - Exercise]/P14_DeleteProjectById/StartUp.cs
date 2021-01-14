using System;
using System.Linq;
using System.Text;
using EntityFrameworkIntroductionExercise.Data;

namespace P14_DeleteProjectById
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var db = new SoftUniContext();

            using (db)
            {
                Console.WriteLine(DeleteProjectById(db));
            }
        }

        public static string DeleteProjectById(SoftUniContext context)
        {
            var sb = new StringBuilder();

            using (context)
            {
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
            }

            return sb.ToString().Trim();
        }
    }
}
