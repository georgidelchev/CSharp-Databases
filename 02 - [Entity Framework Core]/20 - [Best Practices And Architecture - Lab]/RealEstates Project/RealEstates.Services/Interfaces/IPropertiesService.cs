using System.Collections.Generic;
using RealEstates.Services.Models;

namespace RealEstates.Services.Interfaces
{
    public interface IPropertiesService
    {
        void Create(string district, string propertyType, string buildingType, int? year, int price, int size, int? floor, int? maxFloors);

        IEnumerable<PropertyViewModel> Search(int minYear, int maxYear, int minSize, int maxSize);

        IEnumerable<PropertyViewModel> SearchByPrice(int minPrice, int maxPrice);

        void UpdateTags(int propertyId);
    }
}