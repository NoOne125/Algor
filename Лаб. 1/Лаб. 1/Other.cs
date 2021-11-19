using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаб._1
{
    class Other
    {
        public static sbyte[,] field_good =
{
            { 0, 1, 2},
            { 3, 4, 5},
            { 6, 7, 8}
        };

        public static sbyte[,] field_jkl = new sbyte[3, 3]
{
            { 0, 0, 0},
            { 0, 0, 0},
            { 0, 0, 0}
};

        public static sbyte[,] field_bad = new sbyte[3,3]
        {
            { 0, 1, 2},
            { 3, 4, 5},
            { 6, 7, 8}
        };

        public static sbyte[,] Replace(sbyte[,] b, int x, int y, int x1, int y1)
        {
            sbyte t = b[x, y];
            b[x, y] = b[x1, y1];
            b[x1, y1] = t;
            return b;
        }
        public static int G = 0;

        public static sbyte[,] Action(sbyte[,] a, int act, int x, int y)
        {
            G++;
            sbyte[,] b = new sbyte[3,3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    b[i, j] = a[i, j];
            int x1 = x, y1 = y;
            switch (act)
            {
                case 0:
                    x1--;
                    break;
                case 1:
                    y1++;
                    break;
                case 2:
                    x1++;
                    break;
                case 3:
                    y1--;
                    break;
            }
            return Replace(b, x, y, x1, y1);
        }
    }
}
