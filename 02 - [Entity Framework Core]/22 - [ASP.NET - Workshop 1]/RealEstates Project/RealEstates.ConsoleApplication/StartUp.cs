using System;
using RealEstates.Data;
using Microsoft.EntityFrameworkCore;
using RealEstates.Services.Implementations;
using RealEstates.Services.Interfaces;

namespace RealEstates.ConsoleApplication
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var db = new RealEstateDbContext();

            using (db)
            {
                db.Database.Migrate();

                IPropertiesService propertiesService = new PropertiesService(db);

                //propertiesService.Create("Dianabad", "5-Rooms", "Brick", 2019, 210000, 100, 5, 12);

                //propertiesService.UpdateTags(2);
                //propertiesService.UpdateTags(3);

                IDistrictsService districtsService = new DistrictsService(db);

                var districts = districtsService.GetTopDistrictsByAveragePrice();

                foreach (var district in districts)
                {
                    Console.WriteLine($"{district.Name} => {district.AveragePrice:f2} ({district.MinPrice} - {district.MaxPrice}) => {district.PropertiesCount} properties");
                }
            }
        }
    }
}
