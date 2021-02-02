using RealEstates.Data;
using System.Collections.Generic;
using System.Linq;
using RealEstates.Services.Models;
using RealEstates.Services.Interfaces;

namespace RealEstates.Services.Implementations
{
    public class DistrictsService : IDistrictsService
    {
        private readonly RealEstateDbContext db;

        public DistrictsService(RealEstateDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<DistrictViewModel> GetTopDistrictsByAveragePrice(int count = 10)
        {
            return this.db
                .Districts
                .OrderByDescending(d => d.Properties.Average(p => p.Price))
                .Select(MapToDistrictViewModel())
                .Take(count)
                .ToList();
        }

        public IEnumerable<DistrictViewModel> GetTopDistrictsByNumberOfProperties(int count = 10)
        {
            return this.db
                .Districts
                .OrderBy(d => d.Properties.Count)
                .Select(MapToDistrictViewModel())
                .Take(count)
                .ToList();
        }

        private static System.Linq.Expressions.Expression<System.Func<RealEstates.Models.District, DistrictViewModel>> MapToDistrictViewModel()
        {
            return d => new DistrictViewModel()
            {
                AveragePrice = d.Properties.Average(p => p.Price),
                MaxPrice = d.Properties.Max(p => p.Price),
                MinPrice = d.Properties.Min(p => p.Price),
                Name = d.Name,
                PropertiesCount = d.Properties.Count
            };
        }
    }
}