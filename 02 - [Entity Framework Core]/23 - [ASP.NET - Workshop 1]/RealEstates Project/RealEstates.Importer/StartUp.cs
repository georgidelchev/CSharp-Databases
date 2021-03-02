using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using RealEstates.Data;
using RealEstates.Services.Implementations;
using RealEstates.Services.Interfaces;

namespace RealEstates.Importer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var json = File.ReadAllText("imot.bg-raw-data-2020-07-23.json");

            var serializer = JsonSerializer.Deserialize<IEnumerable<JsonProperty>>(json);

            var db = new RealEstateDbContext();


            IPropertiesService propertiesService = new PropertiesService(db);

            var i = 0;
            foreach (var property in serializer
                .Where(x => x.Price > 1000))
            {
                try
                {
                    Console.WriteLine(++i);
                    propertiesService.Create(property.District,
                        property.Type,
                        property.BuildingType,
                        property.Year,
                        property.Price,
                        property.Size,
                        property.Floor,
                        property.TotalFloors);
                }
                catch
                {
                }
            }
        }
    }
}
