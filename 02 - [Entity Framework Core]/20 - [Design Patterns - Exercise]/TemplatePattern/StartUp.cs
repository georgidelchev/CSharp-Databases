using System;

namespace TemplatePattern
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var twelveGrain = new TwelveGrain();
            var sourDough = new SourDough();
            var wholeWheat = new WholeWheat();

            Delimiter();
            twelveGrain.Make();
            Delimiter();
            sourDough.Make();
            Delimiter();
            wholeWheat.Make();
            Delimiter();
        }

        private static void Delimiter()
            => Console.WriteLine(new string('-', 50));
    }
}
