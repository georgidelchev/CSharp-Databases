using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;
using SoftJail.Data;
using SoftJail.DataProcessor.ExportDto;

namespace SoftJail.DataProcessor
{
    public class Serializer
    {
        public static string ExportPrisonersByCells(SoftJailDbContext context, int[] ids)
        {
            var prisoners = context
                .Prisoners
                .ToList()
                .Where(p => ids.Contains(p.Id))
                .Select(p => new
                {
                    Id = p.Id,
                    Name = p.FullName,
                    CellNumber = p.Cell.CellNumber,
                    Officers = p.PrisonerOfficers.Select(po => new
                    {
                        OfficerName = po.Officer.FullName,
                        Department = po.Officer.Department.Name
                    })
                        .ToList()
                        .OrderBy(o => o.OfficerName)
                        .ToList(),
                    TotalOfficerSalary = p.PrisonerOfficers.Sum(po => po.Officer.Salary)
                })
                .ToList()
                .OrderBy(p => p.Name)
                .ThenBy(p => p.Id)
                .ToList();

            var json = JsonConvert.SerializeObject(prisoners, Formatting.Indented);

            return json;
        }

        public static string ExportPrisonersInbox(SoftJailDbContext context, string prisonersNames)
        {
            var sb = new StringBuilder();

            var prisonersList = prisonersNames
                .Split(',')
                .ToList();

            var prisoners = context
                .Prisoners
                .ToList()
                .Where(p => prisonersList.Contains(p.FullName))
                .Select(p => new ExportPrisonerDto()
                {
                    Name = p.FullName,
                    IncarcerationDate = p.IncarcerationDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                    Id = p.Id,
                    EncryptedMessages = p.Mails
                        .ToArray()
                        .Select(m => new ExportPrisonerMessageDto()
                        {
                            Description = ReverseString(m.Description)
                        })
                        .ToArray()
                })
                .ToList()
                .OrderBy(p => p.Name)
                .ThenBy(p => p.Id)
                .ToList();

            var serializer = new XmlSerializer(typeof(List<ExportPrisonerDto>), new XmlRootAttribute("Prisoners"));

            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            var writer = new StringWriter(sb);

            using (writer)
            {
                serializer.Serialize(writer, prisoners, namespaces);
            }

            return sb.ToString().Trim();
        }

        private static string ReverseString(string s)
        {
            var charArray = s.ToCharArray();

            Array.Reverse(charArray);

            return new string(charArray);
        }
    }
}