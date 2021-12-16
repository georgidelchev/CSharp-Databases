namespace Artillery.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Artillery.Data;
    using Artillery.Data.Models;
    using Artillery.Data.Models.Enums;
    using Artillery.DataProcessor.ImportDto;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage =
                "Invalid data.";
        private const string SuccessfulImportCountry =
            "Successfully import {0} with {1} army personnel.";
        private const string SuccessfulImportManufacturer =
            "Successfully import manufacturer {0} founded in {1}.";
        private const string SuccessfulImportShell =
            "Successfully import shell caliber #{0} weight {1} kg.";
        private const string SuccessfulImportGun =
            "Successfully import gun {0} with a total weight of {1} kg. and barrel length of {2} m.";

        public static string ImportCountries(ArtilleryContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(List<ImportCountriesDto>), new XmlRootAttribute("Countries"));

            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            var reader = new StringReader(xmlString);

            using (reader)
            {
                var countriesDtos = (List<ImportCountriesDto>)serializer.Deserialize(reader);

                var countriesToAdd = new List<Country>();

                foreach (var countryDto in countriesDtos)
                {
                    if (!IsValid(countryDto))
                    {
                        sb.AppendLine(ErrorMessage);

                        continue;
                    }

                    var country = new Country()
                    {
                        ArmySize = countryDto.ArmySize,
                        CountryName = countryDto.CountryName,
                    };

                    countriesToAdd.Add(country);
                    sb.AppendLine(string.Format(SuccessfulImportCountry, country.CountryName, country.ArmySize));
                }

                context.AddRange(countriesToAdd);
                context.SaveChanges();

                return sb.ToString().Trim();
            }
        }

        public static string ImportManufacturers(ArtilleryContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(List<ImportManufacturersDto>), new XmlRootAttribute("Manufacturers"));

            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            var reader = new StringReader(xmlString);

            using (reader)
            {
                var manufacturersDtos = (List<ImportManufacturersDto>)serializer.Deserialize(reader);

                var manufacturersToAdd = new List<Manufacturer>();

                foreach (var manufacturerDto in manufacturersDtos)
                {
                    if (!IsValid(manufacturerDto) ||
                        manufacturersToAdd.Any(m => m.ManufacturerName == manufacturerDto.ManufacturerName))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    var founded = manufacturerDto
                        .Founded
                        .Split(", ")
                        .TakeLast(2);

                    var manufacturer = new Manufacturer()
                    {
                        Founded = string.Join(", ", founded),
                        ManufacturerName = manufacturerDto.ManufacturerName,
                    };

                    manufacturersToAdd.Add(manufacturer);
                    sb.AppendLine(string.Format(SuccessfulImportManufacturer, manufacturer.ManufacturerName, manufacturer.Founded));
                }

                context.AddRange(manufacturersToAdd);
                context.SaveChanges();

                return sb.ToString().Trim();
            }
        }

        public static string ImportShells(ArtilleryContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(List<ImportShellsDto>), new XmlRootAttribute("Shells"));

            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            var reader = new StringReader(xmlString);

            using (reader)
            {
                var shellsDtos = (List<ImportShellsDto>)serializer.Deserialize(reader);

                var shellsToAdd = new List<Shell>();

                foreach (var shellDto in shellsDtos)
                {
                    if (!IsValid(shellDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    var shell = new Shell()
                    {
                        Caliber = shellDto.Caliber,
                        ShellWeight = shellDto.ShellWeight,
                    };

                    shellsToAdd.Add(shell);
                    sb.AppendLine(string.Format(SuccessfulImportShell, shell.Caliber, shell.ShellWeight));
                }

                context.AddRange(shellsToAdd);
                context.SaveChanges();

                return sb.ToString().Trim();
            }
        }

        public static string ImportGuns(ArtilleryContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var serializer = JsonConvert.DeserializeObject<List<ImportGunsDto>>(jsonString);

            var gunsToAdd = new List<Gun>();

            foreach (var gunDto in serializer)
            {
                if (!IsValid(gunDto) ||
                    !Enum.IsDefined(typeof(GunType), gunDto.GunType))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var gun = new Gun()
                {
                    Range = gunDto.Range,
                    GunType = Enum.Parse<GunType>(gunDto.GunType),
                    BarrelLength = gunDto.BarrelLength,
                    GunWeight = gunDto.GunWeight,
                    ManufacturerId = gunDto.ManufacturerId,
                    NumberBuild = gunDto.NumberBuild,
                    ShellId = gunDto.ShellId,
                };

                foreach (var countryGun in gunDto.Countries)
                {
                    gun.CountriesGuns.Add(new CountryGun()
                    {
                        CountryId = countryGun.Id,
                        GunId = gun.Id,
                    });
                }

                gunsToAdd.Add(gun);
                sb.AppendLine(string.Format(SuccessfulImportGun, gun.GunType.ToString(), gun.GunWeight, gun.BarrelLength));
            }

            context.AddRange(gunsToAdd);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        private static bool IsValid(object obj)
        {
            var validator = new ValidationContext(obj);
            var validationRes = new List<ValidationResult>();

            var result = Validator.TryValidateObject(obj, validator, validationRes, true);
            return result;
        }
    }
}
