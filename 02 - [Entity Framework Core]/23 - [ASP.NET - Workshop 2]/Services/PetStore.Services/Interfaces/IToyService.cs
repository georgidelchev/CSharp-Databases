using PetStore.Services.Models.Toy;

namespace PetStore.Services.Interfaces
{
    public interface IToyService
    {
        void BuyFromDistributor(string name, string description, decimal distributorPrice, double profit, int brandId, int categoryId);

        void Buy(AddingToyServiceModel model);

        void SellToyToUser(int toyId,int userId);

        bool Exists(int toyId);
    }
}