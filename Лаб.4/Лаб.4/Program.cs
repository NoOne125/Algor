using System;
using System.Collections.Generic;

namespace Лаб._4
{
    class Program
    {
        const int N_iter = 120;
        static void Main(string[] args)
        {
            List<Item> items = new List<Item>();
            Random rand = new Random();

            for (int i = 0; i < 150; i++)
            {
                int price = rand.Next(1, 15);
                int weight = rand.Next(1, 20);
                items.Add(new Item(price, weight));
            }

            GenBagAlgo genBagAlgo = new GenBagAlgo(150, 100, items);
            Chromosome solution = genBagAlgo.GetBest(N_iter);
            Console.WriteLine("Спустя " + N_iter + " Итераций: Макс. Цена - " + solution.price + " при Вес - " + solution.weight + "");
        }
    }
}

