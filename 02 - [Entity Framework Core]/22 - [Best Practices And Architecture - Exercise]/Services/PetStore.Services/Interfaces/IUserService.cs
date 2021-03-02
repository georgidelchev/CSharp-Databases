namespace PetStore.Services.Interfaces
{
    public interface IUserService
    {
        void Register(string name, string email);

        bool Exists(int userId);
    }
}