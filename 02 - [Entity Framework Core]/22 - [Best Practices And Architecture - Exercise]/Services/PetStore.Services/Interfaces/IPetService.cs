using System;
using PetStore.Data.Models.Enum;

namespace PetStore.Services.Interfaces
{
    public interface IPetService
    {
        void BuyPet(string name, Gender gender, DateTime dateOfBirth, decimal price, string description, int breedId,
            int categoryId);

        void SellPet(int petId, int userId);

        bool Exists(int petId);
    }
}