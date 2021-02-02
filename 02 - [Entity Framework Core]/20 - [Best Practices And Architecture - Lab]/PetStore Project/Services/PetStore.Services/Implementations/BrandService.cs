using System;
using System.Linq;
using PetStore.Data;
using PetStore.Data.Models;
using System.Collections.Generic;
using PetStore.Data.Configuration;
using PetStore.Services.Models.Brand;
using PetStore.Services.Models.Toy;


namespace PetStore.Services.Implementations
{
    using static DataValidations;

    public class BrandService : IBrandService
    {
        private readonly PetStoreDbContext data;

        public BrandService(PetStoreDbContext data)
        {
            this.data = data;
        }

        public int Create(string name)
        {
            if (name.Length > BRAND_NAME_MAX_LENGTH)
            {
                throw new InvalidOperationException($"Name cannot be more than {BRAND_NAME_MAX_LENGTH} characters");
            }

            if (this.data.Brands.Any(b => b.Name == name))
            {
                throw new InvalidOperationException($"Brand name {name} already exists");
            }

            var brand = new Brand()
            {
                Name = name
            };

            this.data.Brands.Add(brand);

            this.data.SaveChanges();

            return brand.Id;
        }

        public IEnumerable<BrandListingServiceModel> SearchByName(string name)
        {
            return this.data
                .Brands
                .Where(b => b.Name.ToLower().Contains(name.ToLower()))
                .Select(b => new BrandListingServiceModel()
                {
                    Id = b.Id,
                    Name = b.Name
                })
                .ToList();
        }

        public BrandWithToysServiceModel FindByIdWithToys(int id)
        {
            return this.data
                .Brands
                .Where(b => b.Id == id)
                .Select(b => new BrandWithToysServiceModel()
                {
                    Name = b.Name,
                    Toys = b.Toys
                            .Select(t => new ToyListingServiceModel()
                            {
                                Id = t.Id,
                                Name = t.Name,
                                Price = t.Price,
                                TotalOrders = t.Orders.Count
                            })
                })
                .FirstOrDefault();
        }
    }
}