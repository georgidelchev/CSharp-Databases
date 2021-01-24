using System;
using AutoMapper;
using System.Linq;
using AutoMapper.QueryableExtensions;
using FastFood.Data;
using FastFood.Models;
using Microsoft.AspNetCore.Mvc;
using FastFood.Web.ViewModels.Orders;

namespace FastFood.Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly FastFoodContext context;
        private readonly IMapper mapper;

        public OrdersController(FastFoodContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IActionResult Create()
        {
            var viewOrder = new CreateOrderViewModel
            {
                Items = this.context
                    .Items
                    .ProjectTo<CreateOrderItemViewModel>(this.mapper.ConfigurationProvider)
                    .ToList(),

                Employees = this.context
                    .Employees
                    .ProjectTo<CreateOrderEmployeeViewModel>(this.mapper.ConfigurationProvider)
                    .ToList()
            };

            return this.View(viewOrder);
        }

        [HttpPost]
        public IActionResult Create(CreateOrderInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.RedirectToAction("Error", "Home");
            }

            var order = this.mapper.Map<Order>(model);
            var orderItem = this.mapper.Map<OrderItem>(model);
            orderItem.Order = order;

            this.context.Orders.Add(order);
            this.context.OrderItems.Add(orderItem);

            this.context.SaveChanges();

            return this.RedirectToAction("All", "Orders");
        }

        public IActionResult All()
        {
            var orders = this.context
                .Orders
                .ProjectTo<OrderAllViewModel>(this.mapper.ConfigurationProvider)
                .ToList();

            return this.View(orders);
        }
    }
}
