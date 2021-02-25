using System;
using PetClinic.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;
using PetClinic.DataProcessor.Dto.Import;
using PetClinic.Models;

namespace PetClinic.DataProcessor
{
    public class Deserializer
    {
        private const string SuccessMessage = "Record {0} successfully imported.";

        private const string SuccessMessageAnimal = "Record {0} Passport №: {1} successfully imported.";

        private const string SuccessMessageProcedure = "Record successfully imported.";

        private const string ErrorMessage = "Error: Invalid data.";

        public static string ImportAnimalAids(PetClinicContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var serializer = JsonConvert.DeserializeObject<List<ImportAnimalAidsDto>>(jsonString);

            var animalAidsToAdd = new List<AnimalAid>();

            foreach (var animalAidsDto in serializer)
            {
                if (!IsValid(animalAidsDto))
                {
                    sb.AppendLine(ErrorMessage);

                    continue;
                }

                if (animalAidsToAdd.Any(aa => aa.Name == animalAidsDto.Name))
                {
                    sb.AppendLine(ErrorMessage);

                    continue;
                }

                var animalAid = new AnimalAid()
                {
                    Name = animalAidsDto.Name,
                    Price = animalAidsDto.Price
                };

                animalAidsToAdd.Add(animalAid);

                sb.AppendLine(string.Format(SuccessMessage, animalAid.Name));
            }

            context.AnimalAids.AddRange(animalAidsToAdd);

            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportAnimals(PetClinicContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var serializer = JsonConvert.DeserializeObject<List<ImportAnimalDto>>(jsonString);

            var animalsToAdd = new List<Animal>();

            foreach (var animalDto in serializer)
            {
                if (!IsValid(animalDto) ||
                    !IsValid(animalDto.Passport))
                {
                    sb.AppendLine(ErrorMessage);

                    continue;
                }

                if (context.Passports.Any(p => p.SerialNumber == animalDto.Passport.SerialNumber))
                {
                    sb.AppendLine(ErrorMessage);

                    continue;
                }

                DateTime registrationDate;

                var isRegistrationDateValid = DateTime.TryParseExact(animalDto.Passport.RegistrationDate, "dd-MM-yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out registrationDate);

                if (!isRegistrationDateValid)
                {
                    sb.AppendLine(ErrorMessage);

                    continue;
                }

                var passport = new Passport()
                {
                    OwnerName = animalDto.Passport.OwnerName,
                    OwnerPhoneNumber = animalDto.Passport.OwnerPhoneNumber,
                    RegistrationDate = registrationDate,
                    SerialNumber = animalDto.Passport.SerialNumber
                };

                context.Passports.Add(passport);

                context.SaveChanges();

                var animal = new Animal()
                {
                    Name = animalDto.Name,
                    Age = animalDto.Age,
                    Type = animalDto.Type,
                    PassportSerialNumber = animalDto.Passport.SerialNumber,
                };

                animalsToAdd.Add(animal);

                sb.AppendLine(string.Format(SuccessMessageAnimal, animalDto.Name, animalDto.Passport.SerialNumber));
            }

            context.Animals.AddRange(animalsToAdd);

            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportVets(PetClinicContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(List<ImportVetDto>), new XmlRootAttribute("Vets"));

            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            var reader = new StringReader(xmlString);

            var vetsToAdd = new List<Vet>();

            using (reader)
            {
                var vetDtos = (List<ImportVetDto>)serializer.Deserialize(reader);

                foreach (var vetDto in vetDtos)
                {
                    if (!IsValid(vetDto))
                    {
                        sb.AppendLine(ErrorMessage);

                        continue;
                    }

                    if (vetsToAdd.Any(v => v.PhoneNumber == vetDto.PhoneNumber))
                    {
                        sb.AppendLine(ErrorMessage);

                        continue;
                    }

                    var vet = new Vet()
                    {
                        Name = vetDto.Name,
                        PhoneNumber = vetDto.PhoneNumber,
                        Age = vetDto.Age,
                        Profession = vetDto.Profession
                    };

                    vetsToAdd.Add(vet);

                    sb.AppendLine(string.Format(SuccessMessage, vet.Name));
                }

                context.Vets.AddRange(vetsToAdd);

                context.SaveChanges();

                return sb.ToString().Trim();
            }
        }

        public static string ImportProcedures(PetClinicContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(List<ImportProcedureDto>), new XmlRootAttribute("Procedures"));

            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            var proceduresToAdd = new List<Procedure>();

            var reader = new StringReader(xmlString);

            using (reader)
            {
                var procedureDtos = (List<ImportProcedureDto>)serializer.Deserialize(reader);

                foreach (var procedureDto in procedureDtos)
                {
                    if (!IsValid(procedureDto))
                    {
                        sb.AppendLine(ErrorMessage);

                        continue;
                    }

                    var vet = context.Vets.FirstOrDefault(v => v.Name == procedureDto.Vet);

                    var animal = context.Animals.FirstOrDefault(a => a.PassportSerialNumber == procedureDto.Animal);

                    DateTime dateTime;

                    var isDateTimeValid = DateTime.TryParseExact(procedureDto.DateTime, "dd-MM-yyyy",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime);

                    if (vet == null || animal == null | !isDateTimeValid)
                    {
                        sb.AppendLine(ErrorMessage);

                        continue;
                    }

                    var procedure = new Procedure()
                    {
                        Animal = animal,
                        Vet = vet,
                        DateTime = dateTime,
                    };

                    foreach (var animalAidDto in procedureDto.AnimalAids)
                    {
                        if (!IsValid(animalAidDto))
                        {
                            sb.AppendLine(ErrorMessage);

                            continue;
                        }

                        var animalAid = context.AnimalAids.FirstOrDefault(aa => aa.Name == animalAidDto.Name);

                        var isAnimalAidExisting = procedure.ProcedureAnimalAids.Any(p => p.AnimalAid.Name == animalAid.Name);

                        if (animalAid == null || isAnimalAidExisting)
                        {
                            sb.AppendLine(ErrorMessage);

                            continue;
                        }

                        var procedureAnimalAid = new ProcedureAnimalAid()
                        {
                            Procedure = procedure,
                            AnimalAid = animalAid
                        };

                        procedure.ProcedureAnimalAids.Add(procedureAnimalAid);
                    }

                    proceduresToAdd.Add(procedure);

                    sb.AppendLine(string.Format(SuccessMessageProcedure));
                }

                context.Procedures.AddRange(proceduresToAdd);

                context.SaveChanges();

                return sb.ToString().Trim();
            }
        }

        public static bool IsValid(object obj)
        {
            ValidationContext validationContext = new ValidationContext(obj);
            List<ValidationResult> validationResults = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, validationResults, true);
        }
    }
}