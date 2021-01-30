using System;

namespace Command
{
    public class Product
    {
        public Product(string name, int price)
        {
            this.Name = name;
            this.Price = price;
        }

        public string Name { get; set; }

        public int Price { get; set; }

        public void IncreasePrice(int amount)
        {
            this.Price += amount;
            Console.WriteLine($"The price for the {this.Name} has need increased by {amount}$.");
        }

        public void DecreasePrice(int amount)
        {
            this.Price -= amount;
            Console.WriteLine($"The price for the {this.Name} has need decreased by {amount}$.");
        }

        public override string ToString()
        {
            return $"Current price for the {this.Name} product is {this.Price}$.";
        }
    }
}