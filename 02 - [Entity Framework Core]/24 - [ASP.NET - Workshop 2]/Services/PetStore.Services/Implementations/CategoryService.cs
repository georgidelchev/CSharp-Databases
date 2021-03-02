using System.Linq;
using PetStore.Data;
using PetStore.Data.Models;
using System.Collections.Generic;
using PetStore.Services.Interfaces;
using PetStore.Services.Models.Category;

namespace PetStore.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly PetStoreDbContext data;

        public CategoryService(PetStoreDbContext data)
        {
            this.data = data;
        }

        public void Create(CreateCategoryServiceModel model)
        {
            var category = new Category()
            {
                Name = model.Name,
                Description = model.Description
            };

            this.data.Categories.Add(category);

            this.data.SaveChanges();
        }

        public DetailsCategoryServiceModel GetById(int id)
        {
            var category = this.data
                .Categories
                .Find(id);

            var detailsCategoryServiceModel = new DetailsCategoryServiceModel()
            {
                Id = category?.Id,
                Name = category?.Name,
                Description = category?.Description
            };

            return detailsCategoryServiceModel;
        }

        public void Edit(EditCategoryServiceModel model)
        {
            var category = this.data
                .Categories
                .Find(model.Id);

            category.Name = model.Name;
            category.Description = model.Description;

            this.data.SaveChanges();
        }

        public bool Remove(int? id)
        {
            var category = this.data
                .Categories
                .Find(id);

            if (category == null)
            {
                return false;
            }

            this.data.Categories.Remove(category);
            var deletedEntitiesCount = this.data.SaveChanges();

            return deletedEntitiesCount != 0;
        }

        public IEnumerable<AllCategoriesServiceModel> All()
        {
            return this.data
                .Categories
                .Select(c => new AllCategoriesServiceModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                })
                .ToList();
        }

        public bool Exists(int categoryId)
        {
            return this.data
                .Categories
                .Any(c => c.Id == categoryId);
        }
    }
}