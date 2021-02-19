using System;
using System.Text;
using SoftJail.Data;
using Newtonsoft.Json;
using SoftJail.Data.Models;
using System.Globalization;
using System.Collections.Generic;
using SoftJail.DataProcessor.ImportDto;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using SoftJail.Data.Models.Enums;

namespace SoftJail.DataProcessor
{
    public class Deserializer
    {
        private const string ERROR_MESSAGE = "Invalid Data";

        private const string SUCCESSFULLY_ADDED_DEPARTMENT = "Imported {0} with {1} cells";

        private const string SUCCESSFULLY_ADDED_PRISONER = "Imported {0} {1} years old";

        private const string SUCCESSFULLY_ADDED_OFFICER = "Imported {0} ({1} prisoners)";

        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var serializer = JsonConvert.DeserializeObject<List<ImportDepartmentDto>>(jsonString);

            var departmentsToAdd = new List<Department>();

            foreach (var departmentDto in serializer)
            {
                if (!IsValid(departmentDto))
                {
                    sb.AppendLine(ERROR_MESSAGE);

                    continue;
                }

                var department = new Department()
                {
                    Name = departmentDto.Name
                };

                foreach (var cellDto in departmentDto.Cells)
                {
                    if (!IsValid(cellDto))
                    {
                        sb.AppendLine(ERROR_MESSAGE);

                        break;
                    }

                    var cell = new Cell()
                    {
                        CellNumber = cellDto.CellNumber,
                        HasWindow = cellDto.HasWindow,
                        Department = department
                    };

                    department.Cells.Add(cell);
                }

                if (department.Cells.Count != 0)
                {
                    departmentsToAdd.Add(department);

                    sb.AppendLine(string.Format(SUCCESSFULLY_ADDED_DEPARTMENT, department.Name, department.Cells.Count));
                }
            }

            context.Departments.AddRange(departmentsToAdd);

            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var serializer = JsonConvert.DeserializeObject<List<ImportPrisonerDto>>(jsonString);

            var prisonersToAdd = new List<Prisoner>();

            foreach (var prisonerDto in serializer)
            {
                if (!IsValid(prisonerDto))
                {
                    sb.AppendLine(ERROR_MESSAGE);

                    continue;
                }

                DateTime incarcerationDate;

                var isIncarcerationDateValid = DateTime.TryParseExact(prisonerDto.IncarcerationDate, "dd/MM/yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out incarcerationDate);

                if (!isIncarcerationDateValid)
                {
                    continue;
                }

                DateTime? releaseDate = null;

                if (prisonerDto.ReleaseDate != null)
                {
                    DateTime releaseDateValue;

                    var isReleaseDateValid = DateTime.TryParseExact(prisonerDto.ReleaseDate, "dd/MM/yyyy",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out releaseDateValue);

                    if (!isReleaseDateValid)
                    {
                        continue;
                    }
                }

                var prisoner = new Prisoner()
                {
                    IncarcerationDate = incarcerationDate,
                    ReleaseDate = releaseDate,
                    FullName = prisonerDto.FullName,
                    Age = prisonerDto.Age,
                    Nickname = prisonerDto.Nickname,
                    CellId = prisonerDto.CellId,
                    Bail = prisonerDto.Bail
                };

                foreach (var mailDto in prisonerDto.Mails)
                {
                    if (!IsValid(mailDto))
                    {
                        sb.AppendLine(ERROR_MESSAGE);

                        break;
                    }

                    var mail = new Mail()
                    {
                        Prisoner = prisoner,
                        Address = mailDto.Address,
                        Description = mailDto.Description,
                        Sender = mailDto.Sender
                    };

                    prisoner.Mails.Add(mail);
                }

                prisonersToAdd.Add(prisoner);

                sb.AppendLine(string.Format(SUCCESSFULLY_ADDED_PRISONER, prisonerDto.FullName, prisoner.Age));
            }

            context.Prisoners.AddRange(prisonersToAdd);

            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(List<ImportOfficerDto>), new XmlRootAttribute("Officers"));

            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            var reader = new StringReader(xmlString);

            using (reader)
            {
                var officerDtos = (List<ImportOfficerDto>)serializer.Deserialize(reader);

                var officersToAdd = new List<Officer>();

                foreach (var officerDto in officerDtos)
                {
                    if (!IsValid(officerDto))
                    {
                        sb.AppendLine(ERROR_MESSAGE);

                        continue;
                    }

                    Position position;

                    var isPositionValid = Enum.TryParse(officerDto.Position, out position);

                    if (!isPositionValid)
                    {
                        sb.AppendLine(ERROR_MESSAGE);

                        continue;
                    }

                    Weapon weapon;

                    var isWeaponValid = Enum.TryParse(officerDto.Weapon, out weapon);

                    if (!isWeaponValid)
                    {
                        sb.AppendLine(ERROR_MESSAGE);

                        continue;
                    }

                    var officer = new Officer()
                    {
                        FullName = officerDto.FullName,
                        DepartmentId = officerDto.DepartmentId,
                        Position = position,
                        Salary = officerDto.Money,
                        Weapon = weapon
                    };

                    foreach (var prisonerDto in officerDto.Prisoners)
                    {
                        var prisoner = new OfficerPrisoner()
                        {
                            Officer = officer,
                            PrisonerId = prisonerDto.Id
                        };

                        officer.OfficerPrisoners.Add(prisoner);
                    }

                    officersToAdd.Add(officer);

                    sb.AppendLine(string.Format(SUCCESSFULLY_ADDED_OFFICER, officer.FullName,
                        officer.OfficerPrisoners.Count));
                }

                context.Officers.AddRange(officersToAdd);

                context.SaveChanges();

                return sb.ToString().Trim();
            }
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationResult = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResult, true);
            return isValid;
        }
    }
}