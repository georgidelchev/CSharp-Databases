using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PetStore.Services.Interfaces;
using PetStore.Services.Models.Category;
using PetStore.Web.Models.View_Models.Category;

namespace PetStore.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public IActionResult All()
        {
            var categories = this.categoryService
                .All()
                .Select(csm => new AllCategoriesListingViewModel()
                {
                    Id = csm.Id,
                    Name = csm.Name
                })
                .ToList();

            return this.View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(CreateCategoryInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Error", "Home");
            }

            var categoryServiceModel = new CreateCategoryServiceModel()
            {
                Name = model.Name,
                Description = model.Description
            };

            this.categoryService.Create(categoryServiceModel);

            return this.RedirectToAction("All", "Categories");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var category = this.categoryService
                .GetById(id);

            if (category.Name == null)
            {
                return BadRequest();
            }

            var viewModel = new CategoryDetailsViewModel()
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = this.categoryService.GetById(id);

            if (category.Name == null)
            {
                return this.BadRequest();
            }

            var viewModel = new CategoryDetailsViewModel()
            {
                Name = category.Name,
                Description = category.Description
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(CategoryEditInputModel model)
        {
            if (!this.categoryService.Exists(model.Id))
            {
                return this.BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return this.RedirectToAction("Error", "Home");
            }

            var editCategoryServiceModel = new EditCategoryServiceModel()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description
            };

            this.categoryService.Edit(editCategoryServiceModel);

            return this.RedirectToAction("Details", "Categories", new
            {
                id = editCategoryServiceModel.Id
            });
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var category = this.categoryService.GetById(id);

            if (category.Name == null)
            {
                return this.BadRequest();
            }

            var categoryDetailsViewModel = new CategoryDetailsViewModel()
            {
                Id = category?.Id,
                Name = category.Name,
                Description = category.Description
            };

            return this.View(categoryDetailsViewModel);
        }

        [HttpPost]
        public IActionResult Delete(CategoryDetailsViewModel model)
        {
            var success = this.categoryService.Remove(model.Id);

            if (!success)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.RedirectToAction("All", "Categories");
        }
    }
}