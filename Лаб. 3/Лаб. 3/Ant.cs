using System;
using System.Collections.Generic;
using System.Text;

namespace Лаб._3
{
    class Ant
    {
        const int a = 3;
        const int b = 2;
        const double p = 0.6;

        public List<int> road = new List<int>();
        public int L = 0;

        public static double Chance(double pheromone, int edge)
        {
            return (Math.Pow(pheromone, a) + 1 / Math.Pow(edge, b));
        }

        public static double Update_ph(double start, double Sum_add)
        {
            return start * (1 - p) + Sum_add;
        }

    }
}
