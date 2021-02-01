using System.Collections.Generic;
using PetStore.Services.Models.Category;

namespace PetStore.Services.Interfaces
{
    public interface ICategoryService
    {
        void Create(CreateCategoryServiceModel model);

        DetailsCategoryServiceModel GetById(int id);

        void Edit(EditCategoryServiceModel model);

        bool Remove(int? id);

        IEnumerable<AllCategoriesServiceModel> All();

        bool Exists(int categoryId);
    }
}