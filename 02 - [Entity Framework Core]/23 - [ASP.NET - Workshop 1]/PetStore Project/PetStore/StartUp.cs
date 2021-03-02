using System;
using System.Linq;
using PetStore.Data;
using PetStore.Data.Models;
using PetStore.Data.Models.Enum;
using PetStore.Services.Implementations;

namespace PetStore
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var data = new PetStoreDbContext();

            using (data)
            {
                // var brandService = new BrandService(data);

                // var brandWithToys = brandService.FindByIdWithToys(1);

                // var foodService = new FoodService(data);

                // foodService.BuyFromDistributor("Cat Food", 0.350,  1.1m, 0.3, DateTime.Now, 1, 1);

                // var toyService = new ToyService(data);

                // toyService.BuyFromDistributor("Cat Toy", null, 3.5m,  0.3, 1, 1);

                // var userService = new UserService(data);
                // var foodService = new FoodService(data, userService);

                // userService.Register("Ivan Ivanov Ivanov",  "ivan@gmail.com");

                // foodService.SellFoodToUser(1, 1);

                //// var userService = new UserService(data);
                // var toyService = new ToyService(data, userService);

                // toyService.SellToyToUser(1, 1);

                //// var breedService = new BreedService(data);

                //breedService.Add("Persian");

                //// var categoryService = new CategoryService(data);

                //// var petService = new PetService(data, breedService, categoryService, userService);

                //// petService.BuyPet("Ivcho", Gender.Male, DateTime.Now, 0m, null, 1, 1);

                //// petService.SellPet(1, 1);

                // Add breeds
                //for (int i = 0; i < 10; i++)
                //{
                //    var breed = new Breed()
                //    {
                //        Name = $"Breed {i}",
                //    };

                //    data.Breeds.Add(breed);
                //}

                //data.SaveChanges();

                // Add categories
                //for (int i = 0; i < 30; i++)
                //{
                //    var category = new Category()
                //    {
                //        Name = $"Category {i}",
                //        Description = $"Description {i}"
                //    };

                //    data.Categories.Add(category);
                //}

                data.SaveChanges();

                // Add pets
                for (int i = 0; i < 100; i++)
                {
                    var categoryId = data
                        .Categories
                        .OrderBy(c => Guid.NewGuid())
                        .Select(c => c.Id)
                        .First();

                    var breedId = data
                        .Breeds
                        .OrderBy(c => Guid.NewGuid())
                        .Select(c => c.Id)
                        .First();

                    var pet = new Pet()
                    {
                        Name = $"Ivan {i}",
                        DateOfBirth = DateTime.UtcNow.AddDays(-60),
                        Price = 50 + i,
                        Gender = (Gender)(i % 2),
                        Description = $"Some description! {i}",
                        CategoryId = categoryId,
                        BreedId = breedId
                    };

                    data.Pets.Add(pet);
                }

                data.SaveChanges();
            }
        }
    }
}
