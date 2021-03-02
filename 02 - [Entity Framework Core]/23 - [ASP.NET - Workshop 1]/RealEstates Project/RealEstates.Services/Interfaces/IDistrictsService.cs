using System.Collections.Generic;
using RealEstates.Services.Models;

namespace RealEstates.Services.Interfaces
{
    public interface IDistrictsService
    {
        IEnumerable<DistrictViewModel> GetTopDistrictsByAveragePrice(int count = 10);

        IEnumerable<DistrictViewModel> GetTopDistrictsByNumberOfProperties(int count = 10);
    }
}