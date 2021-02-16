using System;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using TeisterMask.Data;
using System.Globalization;
using TeisterMask.Data.Models;
using System.Xml.Serialization;
using System.Collections.Generic;
using TeisterMask.Data.Models.Enums;
using TeisterMask.DataProcessor.ImportDto;
using System.ComponentModel.DataAnnotations;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace TeisterMask.DataProcessor
{
    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedProject
            = "Successfully imported project - {0} with {1} tasks.";

        private const string SuccessfullyImportedEmployee
            = "Successfully imported employee - {0} with {1} tasks.";

        public static string ImportProjects(TeisterMaskContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(List<ImportProjectDto>), new XmlRootAttribute("Projects"));

            var reader = new StringReader(xmlString);

            var projectsToAdd = new List<Project>();

            using (reader)
            {
                var projectDtos = (List<ImportProjectDto>)serializer.Deserialize(reader);

                foreach (var project in projectDtos)
                {
                    if (!IsValid(project))
                    {
                        sb.AppendLine(ErrorMessage);

                        continue;
                    }

                    DateTime projectOpenDate;

                    var isOpenDateValid = DateTime.TryParseExact(project.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out projectOpenDate);

                    if (!isOpenDateValid)
                    {
                        sb.AppendLine(ErrorMessage);

                        continue;
                    }

                    DateTime? projectDueDate = null;

                    if (!string.IsNullOrEmpty(project.DueDate))
                    {
                        DateTime projectDueDateValue;

                        var isProjectDueDateValid = DateTime.TryParseExact(project.DueDate, "dd/MM/yyyy",
                            CultureInfo.InvariantCulture, DateTimeStyles.None, out projectDueDateValue);

                        if (!isProjectDueDateValid)
                        {
                            sb.AppendLine(ErrorMessage);

                            continue;
                        }

                        projectDueDate = projectDueDateValue;
                    }

                    var projectToAdd = new Project()
                    {
                        Name = project.Name,
                        OpenDate = projectOpenDate,
                        DueDate = projectDueDate
                    };

                    foreach (var task in project.Tasks)
                    {
                        if (!IsValid(task))
                        {
                            sb.AppendLine(ErrorMessage);

                            continue;
                        }

                        DateTime taskOpenDate;

                        bool isTaskOpenDateValid = DateTime.TryParseExact(task.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                            DateTimeStyles.None, out taskOpenDate);

                        if (!isTaskOpenDateValid)
                        {
                            sb.AppendLine(ErrorMessage);

                            continue;
                        }

                        if (taskOpenDate < projectToAdd.OpenDate)
                        {
                            sb.AppendLine(ErrorMessage);

                            continue;
                        }

                        DateTime taskDueDate;

                        var isTaskDueDateValid = DateTime.TryParseExact(task.DueDate, "dd/MM/yyyy",
                            CultureInfo.InvariantCulture, DateTimeStyles.None, out taskDueDate);

                        if (!isTaskDueDateValid)
                        {
                            sb.AppendLine(ErrorMessage);

                            continue;
                        }

                        if (projectDueDate.HasValue)
                        {
                            if (taskDueDate > projectToAdd.DueDate)
                            {
                                sb.AppendLine(ErrorMessage);

                                continue;
                            }
                        }

                        var taskToAdd = new Task()
                        {
                            Name = task.Name,
                            OpenDate = taskOpenDate,
                            DueDate = taskDueDate,
                            ExecutionType = (ExecutionType)task.ExecutionType,
                            LabelType = (LabelType)task.LabelType
                        };

                        projectToAdd.Tasks.Add(taskToAdd);
                    }

                    projectsToAdd.Add(projectToAdd);

                    sb.AppendLine(string.Format(SuccessfullyImportedProject, projectToAdd.Name, projectToAdd.Tasks.Count));
                }

                context.Projects.AddRange(projectsToAdd);

                context.SaveChanges();

                return sb.ToString().Trim();
            }
        }

        public static string ImportEmployees(TeisterMaskContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var employeeDtos = JsonConvert.DeserializeObject<List<ImportEmployeeDto>>(jsonString);

            var employeesToAdd = new List<Employee>();

            foreach (var employee in employeeDtos)
            {
                if (!IsValid(employee))
                {
                    sb.AppendLine(ErrorMessage);

                    continue;
                }

                if (employee.Username.Any(ch => !char.IsLetterOrDigit(ch)))
                {
                    sb.AppendLine(ErrorMessage);
                }

                var employeeToAdd = new Employee()
                {
                    Username = employee.Username,
                    Email = employee.Email,
                    Phone = employee.Phone
                };

                foreach (var taskId in employee.Tasks.Distinct())
                {
                    if (context.Tasks.All(t => t.Id != taskId))
                    {
                        sb.AppendLine(ErrorMessage);

                        continue;
                    }

                    var taskToAdd = context.Tasks.FirstOrDefault(t => t.Id == taskId);

                    var employeeTask = new EmployeeTask()
                    {
                        Employee = employeeToAdd,
                        Task = taskToAdd
                    };

                    employeeToAdd.EmployeesTasks.Add(employeeTask);
                }

                employeesToAdd.Add(employeeToAdd);

                sb.AppendLine(string.Format(SuccessfullyImportedEmployee, employeeToAdd.Username,
                    employeeToAdd.EmployeesTasks.Count));
            }

            context.Employees.AddRange(employeesToAdd);

            context.SaveChanges();

            return sb.ToString().Trim();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}