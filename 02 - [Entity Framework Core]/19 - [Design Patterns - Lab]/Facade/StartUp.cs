using System;

namespace Facade
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var car = new CarBuilderFacade()
                .Info
                    .WithType("Audi")
                    .WithColor("Black")
                    .WithNumberOfDoors(5)
                .Built
                    .InCity("Leipzig")
                    .AtAddress("bv. Somewhere 123321")
                .Build();

            Console.WriteLine(car);
        }
    }
}
