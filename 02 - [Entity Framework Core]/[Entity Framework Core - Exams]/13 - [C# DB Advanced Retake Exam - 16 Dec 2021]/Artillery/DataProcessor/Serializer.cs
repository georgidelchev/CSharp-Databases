namespace Artillery.DataProcessor
{
    using Artillery.Data;
    using Artillery.Data.Models.Enums;
    using Artillery.DataProcessor.ExportDto;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    public class Serializer
    {
        public static string ExportShells(ArtilleryContext context, double shellWeight)
        {
            var shells = context
                .Shells
                .ToList()
                .Where(s => s.ShellWeight > shellWeight)
                .Select(s => new
                {
                    s.ShellWeight,
                    s.Caliber,
                    Guns = s.Guns
                    .ToList()
                    .Where(g => g.GunType.ToString() == GunType.AntiAircraftGun.ToString())
                    .Select(g => new
                    {
                        GunType = g.GunType.ToString(),
                        g.GunWeight,
                        g.BarrelLength,
                        Range = g.Range > 3000 ?
                            "Long-range" :
                            "Regular range",
                    })
                    .ToList()
                    .OrderByDescending(g => g.GunWeight)
                    .ToList(),
                })
                .ToList()
                .OrderBy(s => s.ShellWeight)
                .ToList();

            return JsonConvert.SerializeObject(shells, Formatting.Indented);
        }

        public static string ExportGuns(ArtilleryContext context, string manufacturer)
        {
            var guns = context
                .Guns
                .Where(g => g.Manufacturer.ManufacturerName == manufacturer)
                .Select(g => new ExportGunsDto()
                {
                    BarrelLength = g.BarrelLength,
                    Manufacturer = g.Manufacturer.ManufacturerName,
                    GunType = g.GunType.ToString(),
                    GunWeight = g.GunWeight,
                    Range = g.Range,
                    Countries = g.CountriesGuns
                    .Where(cg => cg.Country.ArmySize > 4_500_000)
                    .Select(cg => new ExportGunsCountriesDto
                    {
                        ArmySize = cg.Country.ArmySize,
                        Country = cg.Country.CountryName,
                    })
                    .OrderBy(c => c.ArmySize)
                    .ToList()
                })
                .OrderBy(g => g.BarrelLength)
                .ToList();

            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(List<ExportGunsDto>), new XmlRootAttribute("Guns"));

            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            var writer = new StringWriter(sb);

            using (writer)
            {
                serializer.Serialize(writer, guns, namespaces);

                return sb.ToString().Trim();
            }
        }
    }
}
