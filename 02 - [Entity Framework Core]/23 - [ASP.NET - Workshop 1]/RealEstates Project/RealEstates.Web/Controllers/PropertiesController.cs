using Microsoft.AspNetCore.Mvc;
using RealEstates.Services.Interfaces;

namespace RealEstates.Web.Controllers
{
    public class PropertiesController : Controller
    {
        private readonly IPropertiesService _propertiesService;

        public PropertiesController(IPropertiesService propertiesService)
        {
            this._propertiesService = propertiesService;
        }

        public IActionResult Search()
        {
            return this.View();
        }

        public IActionResult DoSearch(int minPrice, int maxPrice)
        {
            var properties = this._propertiesService
                .SearchByPrice(minPrice, maxPrice);

            return this.View(properties);
        }
    }
}