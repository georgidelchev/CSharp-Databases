using System;
using System.Collections.Generic;

namespace PetStore.Data.Models
{
    public class Food
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Weight { get; set; }

        public decimal DistributorPrice { get; set; }

        public decimal Price { get; set; }

        public DateTime ExpirationDate { get; set; }

        public int BrandId { get; set; }

        public Brand Brand { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public ICollection<FoodOrder> Orders { get; set; }
            = new HashSet<FoodOrder>();

    }
}