using AutoMapper;
using System.Linq;
using FastFood.Data;
using FastFood.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper.QueryableExtensions;
using FastFood.Web.ViewModels.Positions;

namespace FastFood.Web.Controllers
{
    public class PositionsController : Controller
    {
        private readonly FastFoodContext context;
        private readonly IMapper mapper;

        public PositionsController(FastFoodContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(CreatePositionInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Error", "Home");
            }

            var position = this.mapper.Map<Position>(model);

            this.context.Positions.Add(position);

            this.context.SaveChanges();

            return this.RedirectToAction("All", "Positions");
        }

        public IActionResult All()
        {
            var positions = this.context.Positions
                .ProjectTo<PositionsAllViewModel>(mapper.ConfigurationProvider)
                .ToList();

            return this.View(positions);
        }
    }
}
