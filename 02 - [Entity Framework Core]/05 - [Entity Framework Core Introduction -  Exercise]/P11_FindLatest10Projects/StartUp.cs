using System;
using System.Globalization;
using System.Linq;
using System.Text;
using EntityFrameworkIntroductionExercise.Data;

namespace P11_FindLatest10Projects
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var db = new SoftUniContext();

            using (db)
            {
                Console.WriteLine(GetLatestProjects(db));
            }
        }

        public static string GetLatestProjects(SoftUniContext context)
        {
            var sb = new StringBuilder();

            using (context)
            {
                var projects = context.Projects
                    .OrderBy(p => p.Name)
                    .Take(10)
                    .Select(p => new
                    {
                        Name = p.Name,
                        Desctiption = p.Description,
                        StartDate = p.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)
                    })
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
            }

            return sb.ToString().Trim();
        }
    }
}
