using System;

namespace TemplatePattern
{
    public class WholeWheat : Bread
    {
        public override void MixIngredients()
        {
            Console.WriteLine($"Gathering Ingredients for WholeWheat bread!");
        }

        public override void Bake()
        {
            Console.WriteLine($"Baking the WholeWheat bread! (15 minutes)");
        }
    }
}