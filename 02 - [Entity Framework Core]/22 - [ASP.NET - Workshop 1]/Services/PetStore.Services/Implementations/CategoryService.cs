using System.Linq;
using PetStore.Data;
using PetStore.Services.Interfaces;

namespace PetStore.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly PetStoreDbContext data;

        public CategoryService(PetStoreDbContext data)
        {
            this.data = data;
        }

        public bool Exists(int categoryId)
        {
            return this.data
                .Categories
                .Any(c => c.Id == categoryId);
        }
    }
}