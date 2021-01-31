namespace PetStore.Services.Interfaces
{
    public interface IBreedService
    {
        void Add(string name);

        bool Exists(int breedId);
    }
}