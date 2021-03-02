using System;

namespace Singleton
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            // Only one instance!
            // var db = SingletonDataContainer.Instance;
            // var db2 = SingletonDataContainer.Instance;
            // var db3 = SingletonDataContainer.Instance;
            // var db4 = SingletonDataContainer.Instance;

            var db = SingletonDataContainer.Instance;
            Console.WriteLine(db.GetPopulation("Washington, D.C."));

            var db2 = SingletonDataContainer.Instance;
            Console.WriteLine(db.GetPopulation("London"));
        }
    }
}
