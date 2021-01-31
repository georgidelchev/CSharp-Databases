using System;
using PetStore.Data;
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
            }
        }
    }
}
