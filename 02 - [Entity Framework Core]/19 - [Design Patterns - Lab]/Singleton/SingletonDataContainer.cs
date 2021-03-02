using System;
using System.Collections.Generic;
using System.IO;

namespace Singleton
{
    public class SingletonDataContainer : ISingletonContainer
    {
        private readonly Dictionary<string, int> capitals = new Dictionary<string, int>();

        public SingletonDataContainer()
        {
            Console.WriteLine("Initializing singleton object");

            var elements = File.ReadAllLines("capitals.txt");

            for (int i = 0; i < elements.Length; i += 2)
            {
                capitals.Add(elements[i], int.Parse(elements[i + 1]));
            }
        }

        private static SingletonDataContainer instance = new SingletonDataContainer();

        public static SingletonDataContainer Instance => instance;

        public int GetPopulation(string name)
        {
            return capitals[name];
        }
    }
}