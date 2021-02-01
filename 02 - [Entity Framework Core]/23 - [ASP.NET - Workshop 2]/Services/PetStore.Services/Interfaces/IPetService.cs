using System;
using PetStore.Data.Models.Enum;
using System.Collections.Generic;
using PetStore.Services.Models.Pet;

namespace PetStore.Services.Interfaces
{
    public interface IPetService
    {
        IEnumerable<PetListingServiceModel> All(int page = 1);

        PetDetailsServiceModel Details(int id);

        void BuyPet(string name, Gender gender, DateTime dateOfBirth, decimal price, string description, int breedId,
            int categoryId);

        void SellPet(int petId, int userId);

        bool Exists(int petId);

        int Total();

        bool Delete(int id);
    }
}