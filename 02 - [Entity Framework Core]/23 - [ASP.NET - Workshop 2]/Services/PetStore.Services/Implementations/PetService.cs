using System;
using System.Linq;
using PetStore.Data;
using PetStore.Data.Models;
using PetStore.Data.Models.Enum;
using System.Collections.Generic;
using PetStore.Services.Interfaces;
using PetStore.Services.Models.Pet;

namespace PetStore.Services.Implementations
{
    public class PetService : IPetService
    {
        private const int PETS_PAGE_SIZE = 25;

        private readonly PetStoreDbContext data;

        private readonly IBreedService breedService;
        private readonly ICategoryService categoryService;
        private readonly IUserService userService;

        public PetService(PetStoreDbContext data, IBreedService breedService, ICategoryService categoryService, IUserService userService)
        {
            this.data = data;
            this.breedService = breedService;
            this.categoryService = categoryService;
            this.userService = userService;
        }

        public IEnumerable<PetListingServiceModel> All(int page = 1)
        {
            return this.data
                .Pets
                .Skip((page - 1) * PETS_PAGE_SIZE)
                .Take(PETS_PAGE_SIZE)
                .Select(p => new PetListingServiceModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Breed = p.Breed.Name,
                    Category = p.Category.Name
                })
                .ToList();
        }

        public PetDetailsServiceModel Details(int id)
        {
            return this.data
                .Pets
                .Where(p => p.Id == id)
                .Select(p => new PetDetailsServiceModel
                {
                    Name = p.Name,
                    Price = p.Price,
                    Breed = p.Breed.Name,
                    Category = p.Category.Name,
                    DateOfBirth = p.DateOfBirth,
                    Description = p.Description,
                    Gender = p.Gender,
                    Id = p.Id
                })
                .FirstOrDefault();
        }

        public void BuyPet(string name, Gender gender, DateTime dateOfBirth, decimal price, string description, int breedId, int categoryId)
        {
            if (price < 0)
            {
                throw new ArgumentException("Price of the pet cannot be less than zero");
            }

            if (!this.breedService.Exists(breedId))
            {
                throw new ArgumentException("There is no such breed with given id!");
            }

            if (!this.categoryService.Exists(categoryId))
            {
                throw new ArgumentException("There is no such category with given id!");
            }

            var pet = new Pet()
            {
                Name = name,
                Gender = gender,
                DateOfBirth = dateOfBirth,
                Price = price,
                Description = description,
                BreedId = breedId,
                CategoryId = categoryId
            };

            this.data.Pets.Add(pet);

            this.data.SaveChanges();
        }

        public void SellPet(int petId, int userId)
        {
            if (!this.userService.Exists(userId))
            {
                throw new ArgumentException("There is no such user with given id in database!");
            }

            if (!this.Exists(petId))
            {
                throw new ArgumentException("There is no such pet with given id in database!");
            }

            var pet = this.data
                .Pets
                .First(p => p.Id == petId);

            var order = new Order()
            {
                PurchaseDate = DateTime.Now,
                Status = OrderStatus.Done,
                UserId = userId
            };

            this.data.Orders.Add(order);

            pet.Order = order;

            this.data.SaveChanges();
        }

        public bool Exists(int petId)
        {
            return this.data
                .Pets
                .Any(p => p.Id == petId);
        }

        public int Total()
        {
            return this.data.Pets.Count();
        }

        public bool Delete(int id)
        {
            var pet = this.data
                .Pets
                .Find(id);

            if (pet == null)
            {
                return false;
            }

            this.data
                .Pets
                .Remove(pet);

            this.data.SaveChanges();

            return true;
        }
    }
}