using System;

namespace TemplatePattern
{
    public class SourDough : Bread
    {
        public override void MixIngredients()
        {
            Console.WriteLine($"Gathering Ingredients for SourDough bread!");
        }

        public override void Bake()
        {
            Console.WriteLine($"Baking the SourDough bread! (20 minutes)");
        }
    }
}