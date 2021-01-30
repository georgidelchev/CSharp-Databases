using System;

namespace TemplatePattern
{
    public class TwelveGrain : Bread
    {
        public override void MixIngredients()
        {
            Console.WriteLine($"Gathering Ingredients for 12-Grain bread!");
        }

        public override void Bake()
        {
            Console.WriteLine($"Baking the 12-Grain Bread! (25 minutes)");
        }
    }
}