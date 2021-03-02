using System;
using PetStore.Services.Models.Food;

namespace PetStore.Services
{
    public interface IFoodService
    {
        void BuyFromDistributor(string name, double weight, decimal distributorPrice, double profit, DateTime expirationDate, int brandId, int categoryId);

        void Buy(AddingFoodServiceModel model);

        void SellFoodToUser(int foodId, int userId);

        bool Exists(int foodId);
    }
}