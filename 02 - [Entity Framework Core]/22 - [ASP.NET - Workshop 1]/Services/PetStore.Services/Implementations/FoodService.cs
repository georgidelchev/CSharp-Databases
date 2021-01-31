﻿using System;
using System.Linq;
using PetStore.Data;
using PetStore.Data.Models;
using PetStore.Data.Models.Enum;
using PetStore.Services.Interfaces;
using PetStore.Services.Models.Food;

namespace PetStore.Services.Implementations
{
    public class FoodService : IFoodService
    {
        private readonly PetStoreDbContext data;
        private readonly IUserService userService;

        public FoodService(PetStoreDbContext data, IUserService userService)
        {
            this.data = data;
            this.userService = userService;
        }

        public void BuyFromDistributor(string name, double weight, decimal distributorPrice, double profit, DateTime expirationDate, int brandId, int categoryId)
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

            var food = new Food()
            {
                Name = name,
                Weight = weight,
                DistributorPrice = distributorPrice,
                Price = distributorPrice + (distributorPrice * (decimal)profit),
                ExpirationDate = expirationDate,
                BrandId = brandId,
                CategoryId = categoryId
            };

            this.data.Foods.Add(food);

            this.data.SaveChanges();
        }

        public void Buy(AddingFoodServiceModel model)
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

            var food = new Food()
            {
                Name = model.Name,
                Weight = model.Weight,
                DistributorPrice = model.Price,
                Price = model.Price + (model.Price * (decimal)model.Profit),
                BrandId = model.BrandId,
                CategoryId = model.CategoryId
            };

            this.data.Foods.Add(food);

            this.data.SaveChanges();
        }

        public void SellFoodToUser(int foodId, int userId)
        {
            if (!this.Exists(foodId))
            {
                throw new ArgumentException("There is no such food with given id in the database!");
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

            var foodOrder = new FoodOrder()
            {
                FoodId = foodId,
                Order = order
            };

            this.data.Orders.Add(order);

            this.data.FoodOrders.Add(foodOrder);

            this.data.SaveChanges();
        }

        public bool Exists(int foodId)
        {
            return this.data
                .Foods
                .Any(f => f.Id == foodId);
        }
    }
}