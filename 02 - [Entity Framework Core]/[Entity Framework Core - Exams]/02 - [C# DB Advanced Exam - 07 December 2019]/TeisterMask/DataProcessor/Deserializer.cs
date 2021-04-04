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

            using (reader)
            {
                var projectDtos = (List<ImportProjectDto>)serializer.Deserialize(reader);

                foreach (var projectDto in projectDtos)
                {
                    if (!IsValid(projectDto))
                    {
                        sb.AppendLine(ErrorMessage);

                        continue;
                    }

                    DateTime projectOpenDate;
                    DateTime? projectDueDateAsNullable = null;

                    var isProjectOpenDateValid = DateTime.TryParseExact(projectDto.OpenDate, "dd/MM/yyyy",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out projectOpenDate);

                    if (!isProjectOpenDateValid)
                    {
                        sb.AppendLine(ErrorMessage);

                        continue;
                    }

                    if (projectDto.DueDate != null)
                    {
                        DateTime projectDueDate;

                        var isProjectDueDateValid = DateTime.TryParseExact(projectDto.DueDate, "dd/MM/yyyy",
                            CultureInfo.InvariantCulture, DateTimeStyles.None, out projectDueDate);

                        if (!isProjectDueDateValid)
                        {
                            sb.AppendLine(ErrorMessage);

                            continue;
                        }

                        projectDueDateAsNullable = projectDueDate;
                    }

                    var project = new Project()
                    {
                        DueDate = projectDueDateAsNullable,
                        OpenDate = projectOpenDate,
                        Name = projectDto.Name
                    };

                    foreach (var taskDto in projectDto.Tasks)
                    {
                        if (!IsValid(taskDto))
                        {
                            sb.AppendLine(ErrorMessage);

                            continue;
                        }

                        DateTime taskOpenDate;

                        var isTaskOpenDateValid = DateTime.TryParseExact(taskDto.OpenDate, "dd/MM/yyyy",
                            CultureInfo.InvariantCulture, DateTimeStyles.None, out taskOpenDate);

                        if (!isTaskOpenDateValid)
                        {
                            sb.AppendLine(ErrorMessage);

                            continue;
                        }

                        if (taskOpenDate < projectOpenDate)
                        {
                            sb.AppendLine(ErrorMessage);

                            continue;
                        }

                        DateTime taskDueDate;

                        var isTaskDueDateValid = DateTime.TryParseExact(taskDto.DueDate, "dd/MM/yyyy",
                            CultureInfo.InvariantCulture, DateTimeStyles.None, out taskDueDate);

                        if (!isTaskDueDateValid)
                        {
                            sb.AppendLine(ErrorMessage);

                            continue;
                        }

                        if (projectDueDateAsNullable.HasValue)
                        {
                            if (taskDueDate > projectDueDateAsNullable)
                            {
                                sb.AppendLine(ErrorMessage);

                                continue;
                            }
                        }

                        var task = new Task()
                        {
                            DueDate = taskDueDate,
                            Name = taskDto.Name,
                            OpenDate = taskOpenDate,
                            LabelType = (LabelType)taskDto.LabelType,
                            ExecutionType = (ExecutionType)taskDto.ExecutionType
                        };

                        project.Tasks.Add(task);
                    }

                    context.Projects.Add(project);

                    context.SaveChanges();

                    sb.AppendLine(string.Format(SuccessfullyImportedProject, project.Name, project.Tasks.Count));
                }
            }

            return sb.ToString().Trim();
        }

        public static string ImportEmployees(TeisterMaskContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var employeeDtos = JsonConvert.DeserializeObject<List<ImportEmployeeDto>>(jsonString);

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

                    continue;
                }

                var employeeToAdd = new Employee()
                {
                    Username = employee.Username,
                    Email = employee.Email,
                    Phone = employee.Phone
                };

                foreach (var taskId in employee.Tasks.Distinct())
                {
                    var task = context.Tasks
                        .FirstOrDefault(t => t.Id == taskId);

                    if (task == null)
                    {
                        sb.AppendLine(ErrorMessage);

                        continue;
                    }

                    var employeeTask = new EmployeeTask()
                    {
                        Employee = employeeToAdd,
                        Task = task
                    };

                    employeeToAdd.EmployeesTasks.Add(employeeTask);
                }

                context.Employees.Add(employeeToAdd);

                context.SaveChanges();

                sb.AppendLine(string.Format(SuccessfullyImportedEmployee, employeeToAdd.Username,
                    employeeToAdd.EmployeesTasks.Count));
            }

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