using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;
using PetClinic.Data;
using PetClinic.DataProcessor.Dto.Export;

namespace PetClinic.DataProcessor
{
    public class Serializer
    {
        public static string ExportAnimalsByOwnerPhoneNumber(PetClinicContext context, string phoneNumber)
        {
            var animals = context
                .Animals
                .Where(a => a.Passport.OwnerPhoneNumber == phoneNumber)
                .Select(a => new
                {
                    OwnerName = a.Passport.OwnerName,
                    AnimalName = a.Name,
                    Age = a.Age,
                    SerialNumber = a.PassportSerialNumber,
                    RegisteredOn = a.Passport.RegistrationDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture)
                })
                .OrderBy(a => a.Age)
                .ThenBy(a => a.SerialNumber)
                .ToList();

            var json = JsonConvert.SerializeObject(animals, Formatting.Indented);

            return json;
        }

        public static string ExportAllProcedures(PetClinicContext context)
        {
            var sb = new StringBuilder();

            var procedures = context
                .Procedures
                .Select(p => new ExportProcedureDto()
                {
                    OwnerName = p.Animal.Passport.OwnerName,
                    Passport = p.Animal.PassportSerialNumber,
                    DateTime = p.DateTime.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture),
                    AnimalAids = p.ProcedureAnimalAids.Select(paa => new ExportProcedureAnimalAidDto()
                    {
                        Name = paa.AnimalAid.Name,
                        Price = paa.AnimalAid.Price
                    })
                        .ToList()
                })
                .OrderBy(p => p.DateTime)
                .ThenBy(p => p.Passport)
                .ToList();

            var serializer = new XmlSerializer(typeof(List<ExportProcedureDto>), new XmlRootAttribute("Procedures"));

            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            var writer = new StringWriter(sb);

            using (writer)
            {
                serializer.Serialize(writer, procedures, namespaces);

                return sb.ToString().Trim();
            }
        }
    }
}