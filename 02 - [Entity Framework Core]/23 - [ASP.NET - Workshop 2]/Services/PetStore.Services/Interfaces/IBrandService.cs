using System.Collections.Generic;
using PetStore.Services.Models.Brand;

namespace PetStore.Services.Interfaces
{
    public interface IBrandService
    {
        int Create(string name);

        IEnumerable<BrandListingServiceModel> SearchByName(string name);

        BrandWithToysServiceModel FindByIdWithToys(int id);
    }
}