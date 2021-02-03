using System;
using System.Linq;
using RealEstates.Data;
using RealEstates.Models;
using System.Linq.Expressions;
using System.Collections.Generic;
using RealEstates.Services.Models;
using RealEstates.Services.Interfaces;

namespace RealEstates.Services.Implementations
{
    public class PropertiesService : IPropertiesService
    {
        private readonly RealEstateDbContext db;

        public PropertiesService(RealEstateDbContext db)
        {
            this.db = db;
        }

        public void Create(string district, string propertyType, string buildingType, int? year, int price, int size, int? floor,
            int? maxFloors)
        {
            if (district == null)
            {
                throw new ArgumentException(nameof(district));
            }

            var property = new RealEstateProperty()
            {
                Size = size,
                Price = price,
                Year = year < 1800 ? null : year,
                Floor = floor <= 0 ? null : floor,
                TotalNumberOfFloors = maxFloors <= 0 ? null : maxFloors,
            };

            // District Entity
            var districtEntity = this.db
                .Districts
                .FirstOrDefault(d => d.Name.Trim() == district.Trim());

            if (districtEntity == null)
            {
                districtEntity = new District()
                {
                    Name = district
                };
            }

            property.District = districtEntity;

            // Property Type Entity
            var propertyTypeEntity = this.db
                .PropertyTypes
                .FirstOrDefault(pt => pt.Name.Trim() == propertyType.Trim());

            if (propertyTypeEntity == null)
            {
                propertyTypeEntity = new PropertyType()
                {
                    Name = propertyType
                };
            }

            property.PropertyType = propertyTypeEntity;

            // Building Type Entity
            var buildingTypeEntity = this.db
                .BuildingTypes
                .FirstOrDefault(bt => bt.Name.Trim() == buildingType.Trim());

            if (buildingTypeEntity == null)
            {
                buildingTypeEntity = new BuildingType()
                {
                    Name = buildingType
                };
            }

            property.BuildingType = buildingTypeEntity;

            this.db.RealEstateProperties.Add(property);

            this.db.SaveChanges();

            this.UpdateTags(property.Id);
        }

        public IEnumerable<PropertyViewModel> Search(int minYear, int maxYear, int minSize, int maxSize)
        {
            return this.db
                .RealEstateProperties
                .Where(rep => rep.Year >= minYear && rep.Year <= maxYear && rep.Size >= minSize && rep.Size <= maxSize)
                .Select(MapToPropertyViewModel())
                .OrderBy(rep => rep.Price)
                .ToList();
        }

        public IEnumerable<PropertyViewModel> SearchByPrice(int minPrice, int maxPrice)
        {
            return this.db
                .RealEstateProperties
                .Where(rep => rep.Price >= minPrice && rep.Price <= maxPrice)
                .Select(MapToPropertyViewModel())
                .OrderBy(rep => rep.Price)
                .ToList();
        }

        public void UpdateTags(int propertyId)
        {
            var property = this.db
                .RealEstateProperties
                .FirstOrDefault(rep => rep.Id == propertyId);

            property.Tags.Clear();

            if (property.Year.HasValue && property.Year < 1990)
            {
                property.Tags.Add(new RealEstatePropertyTag()
                {
                    Tag = GetOrCreateTag("OldBuilding")
                });
            }

            if (property.Size > 120)
            {
                property.Tags.Add(new RealEstatePropertyTag()
                {
                    Tag = GetOrCreateTag("HugeApartment")
                });
            }

            if (property.Year > 2018 && property.TotalNumberOfFloors > 5)
            {
                property.Tags.Add(new RealEstatePropertyTag()
                {
                    Tag = GetOrCreateTag("HasParking")
                });
            }

            if (property.Floor == property.TotalNumberOfFloors)
            {
                property.Tags.Add(new RealEstatePropertyTag()
                {
                    Tag = GetOrCreateTag("LastFloor")
                });
            }

            if ((property.Price / property.Size) <= 800)
            {
                property.Tags.Add(new RealEstatePropertyTag()
                {
                    Tag = GetOrCreateTag("CheapProperty")
                });
            }

            if ((property.Price / property.Size) > 2000)
            {
                property.Tags.Add(new RealEstatePropertyTag()
                {
                    Tag = GetOrCreateTag("ExpensiveProperty")
                });
            }

            this.db.SaveChanges();
        }

        private static Expression<Func<RealEstateProperty, PropertyViewModel>> MapToPropertyViewModel()
        {
            return rep => new PropertyViewModel()
            {
                Price = rep.Price,
                Year = rep.Year,
                Floor = (rep.Floor ?? 0).ToString() + "/" + (rep.TotalNumberOfFloors ?? 0),
                Size = rep.Size,
                BuildingType = rep.BuildingType.Name,
                District = rep.District.Name,
                PropertyType = rep.PropertyType.Name
            };
        }

        private Tag GetOrCreateTag(string tag)
        {
            var tagEntity = this.db
                .Tags
                .FirstOrDefault(pt => pt.Name.Trim() == tag);

            if (tagEntity == null)
            {
                tagEntity = new Tag()
                {
                    Name = tag
                };
            }

            return tagEntity;
        }
    }
}