using System;

namespace PrototypePattern
{
    public class Sandwich : SandwichPrototype
    {
        private string bread;
        private string meat;
        private string cheese;
        private string veggies;

        public Sandwich(string bread, string meat, string cheese, string veggies)
        {
            this.bread = bread;
            this.meat = meat;
            this.cheese = cheese;
            this.veggies = veggies;
        }

        public override SandwichPrototype Clone()
        {
            var ingredients = GetIngredientsList();

            Console.WriteLine($"Cloning sandwich with ingredients: {ingredients}");

            return this.MemberwiseClone() as SandwichPrototype;
        }

        private string GetIngredientsList()
        {
            return $"{this.bread}, " +
                   $"{this.meat}, " +
                   $"{this.cheese}, " +
                   $"{this.veggies}";
        }
    }
}