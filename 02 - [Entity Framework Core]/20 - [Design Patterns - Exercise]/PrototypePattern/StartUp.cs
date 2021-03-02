using System;

namespace PrototypePattern
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var sandwichMenu = new SandwichMenu();

            sandwichMenu["BLT"] = new Sandwich("Wheat", "Bacon", "", "Lettuce, Tomato");

            sandwichMenu["PB&J"] = new Sandwich("White", "", "", "Peanut Butter, Jelly");

            sandwichMenu["Turkey"] = new Sandwich("Rye", "Turkey", "Swiss", "lettuce, Onion, Tomato");

            sandwichMenu["LoadedBLT"] = new Sandwich("Wheat", "Turkey, Bacon", "American", "Lettuce, Tomato, Onion, Olives");

            sandwichMenu["ThreeMeatCombo"] = new Sandwich("Rye", "Turkey, Ham, Salami", "Provolone", "Lettuce, Onion");

            sandwichMenu["Vegetarian"] = new Sandwich("Wheat", "", "", "Lettuce, Onion, Tomato, Olives, Spinach");

            var bltSandwich = sandwichMenu["BLT"].Clone() as Sandwich;

            Console.WriteLine();

            var threeMeatComboSandwich = sandwichMenu["ThreeMeatCombo"].Clone() as Sandwich;
        }
    }
}
