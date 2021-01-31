using System.Collections.Generic;
using PetStore.Services.Models.Toy;

namespace PetStore.Services.Models.Brand
{
    public class BrandWithToysServiceModel
    {
        public string Name { get; set; }

        public IEnumerable<ToyListingServiceModel> Toys { get; set; }
    }
}