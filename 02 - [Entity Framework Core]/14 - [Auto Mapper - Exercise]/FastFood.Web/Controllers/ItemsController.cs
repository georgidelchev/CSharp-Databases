using System;
using AutoMapper;
using System.Linq;
using FastFood.Data;
using Microsoft.AspNetCore.Mvc;
using FastFood.Web.ViewModels.Items;
using AutoMapper.QueryableExtensions;
using FastFood.Models;


namespace FastFood.Web.Controllers
{
    public class ItemsController : Controller
    {
        private readonly FastFoodContext context;
        private readonly IMapper mapper;

        public ItemsController(FastFoodContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IActionResult Create()
        {
            var categories = this.context.Categories
                .ProjectTo<CreateItemViewModel>(mapper.ConfigurationProvider)
                .ToList();

            return this.View(categories);
        }

        [HttpPost]
        public IActionResult Create(CreateItemInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Error", "Home");
            }

            var item = this.mapper.Map<Item>(model);

            this.context.Items.Add(item);

            this.context.SaveChanges();

            return RedirectToAction("All", "Items");
        }

        public IActionResult All()
        {
            var items = this.context
                .Items
                .ProjectTo<ItemsAllViewModels>(this.mapper.ConfigurationProvider)
                .ToList();

            return this.View(items);
        }
    }
}
