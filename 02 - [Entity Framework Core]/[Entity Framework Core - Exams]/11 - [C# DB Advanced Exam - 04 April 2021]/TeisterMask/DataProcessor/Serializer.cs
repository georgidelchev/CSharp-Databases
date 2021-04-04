using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using TeisterMask.Data;
using TeisterMask.Data.Models.Enums;
using TeisterMask.DataProcessor.ExportDto;
using Formatting = Newtonsoft.Json.Formatting;

namespace TeisterMask.DataProcessor
{
    public class Serializer
    {
        public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        {
            var sb = new StringBuilder();

            var projects = context
                .Projects
                .ToList()
                .Where(p => p.Tasks.Count >= 1)
                .Select(p => new ExportProjectDto()
                {
                    TasksCount = p.Tasks.Count,
                    ProjectName = p.Name,
                    HasEndDate = p.DueDate == null ? "No" : "Yes",
                    Tasks = p.Tasks.Select(t => new ExportTaskDto()
                    {
                        Name = t.Name,
                        Label = t.LabelType.ToString()
                    })
                        .ToList()
                        .OrderBy(t => t.Name)
                        .ToList()
                })
                .ToList()
                .OrderByDescending(t => t.TasksCount)
                .ThenBy(t => t.ProjectName)
                .ToList();

            var serializer = new XmlSerializer(typeof(List<ExportProjectDto>), new XmlRootAttribute("Projects"));

            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            var writer = new StringWriter(sb);

            using (writer)
            {
                serializer.Serialize(writer, projects, namespaces);
            }

            return sb.ToString().Trim();
        }

        public static string ExportMostBusiestEmployees(TeisterMaskContext context, DateTime date)
        {
            var employees = context
                .Employees
                .ToList()
                .Where(e => e.EmployeesTasks.Any(et => et.Task.OpenDate >= date))
                .Select(e => new
                {
                    Username = e.Username,
                    Tasks = e.EmployeesTasks
                        .ToList()
                        .Where(et => et.Task.OpenDate >= date)
                        .OrderByDescending(t => t.Task.DueDate)
                        .ThenBy(t => t.Task.Name)
                        .Select(et => new
                        {
                            TaskName = et.Task.Name,
                            OpenDate = et.Task.OpenDate.ToString("d", CultureInfo.InvariantCulture),
                            DueDate = et.Task.DueDate.ToString("d", CultureInfo.InvariantCulture),
                            LabelType = et.Task.LabelType.ToString(),
                            ExecutionType = et.Task.ExecutionType.ToString()
                        })
                        .ToList()
                })
                .ToList()
                .OrderByDescending(e => e.Tasks.Count)
                .ThenBy(e => e.Username)
                .Take(10)
                .ToList();

            var serializer = JsonConvert.SerializeObject(employees, Formatting.Indented);

            return serializer;
        }
    }
}