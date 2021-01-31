﻿using System;
using System.Linq;
using PetStore.Data;
using PetStore.Data.Models;
using PetStore.Data.Models.Enum;
using PetStore.Services.Models.Toy;
using PetStore.Services.Interfaces;

namespace PetStore.Services.Implementations
{
    public class ToyService : IToyService
    {
        private readonly PetStoreDbContext data;
        private readonly IUserService userService;

        public ToyService(PetStoreDbContext data, IUserService userService)
        {
            this.data = data;
            this.userService = userService;
        }

        public void BuyFromDistributor(string name, string description, decimal distributorPrice, double profit, int brandId,
            int categoryId)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be null or whitespace!");
            }

            if (profit < 0 ||
                profit > 5)
            {
                throw new ArgumentException("Profit must be higher than 0% and lower than 500%");
            }

            var toy = new Toy()
            {
                Name = name,
                Description = description,
                DistributorPrice = distributorPrice,
                Price = distributorPrice + (distributorPrice * (decimal)profit),
                BrandId = brandId,
                CategoryId = categoryId
            };

            this.data.Toys.Add(toy);

            this.data.SaveChanges();
        }

        public void Buy(AddingToyServiceModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                throw new ArgumentException("Name cannot be null or whitespace!");
            }

            if (model.Profit < 0 ||
                model.Profit > 5)
            {
                throw new ArgumentException("Profit must be higher than 0% and lower than 500%");
            }

            var toy = new Toy()
            {
                Name = model.Name,
                Description = model.Description,
                DistributorPrice = model.Price,
                Price = model.Price + (model.Price * (decimal)model.Profit),
                BrandId = model.BrandId,
                CategoryId = model.CategoryId
            };

            this.data.Toys.Add(toy);

            this.data.SaveChanges();
        }

        public void SellToyToUser(int toyId, int userId)
        {
            if (!this.Exists(toyId))
            {
                throw new ArgumentException("There is no such toy with given id in the database!");
            }

            if (!userService.Exists(userId))
            {
                throw new ArgumentException("There is no such user with given id in the database!");
            }

            var order = new Order()
            {
                PurchaseDate = DateTime.UtcNow,
                Status = OrderStatus.Done,
                UserId = userId
            };

            var toyOrder = new ToyOrder()
            {
                ToyId = toyId,
                Order = order
            };

            this.data.Orders.Add(order);

            this.data.ToyOrders.Add(toyOrder);

            this.data.SaveChanges();
        }

        public bool Exists(int toyId)
        {
            return this.data
                .Toys
                .Any(t => t.Id == toyId);;
        }
    }
}