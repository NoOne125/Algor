using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Лаб._1.Other;

namespace Лаб._1
{
    class IDS_m
    {
        public static bool IDS(int depth, Node current, out Node res, ref int N)
        {
            N++;
            bool end = false;
            res = current;
            bool k = true;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    k = k && current.field[i, j] == field_good[i, j];
            if (k)
            {
                end = true;
            }
            else if (depth == current.depth)
            {
                end = false;
            }
            else
            {
                int x = -1, y = -1;
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        if (current.field[i, j] == 0)
                        {
                            x = i;
                            y = j;
                        }
                if (x != 0)
                    if (IDS(depth, new Node(Action(current.field, 0, x, y), 0, current.depth + 1, current.path_cost + 1, current), out res, ref N))
                        end = true;
                if (end)
                    return end;
                if (y != 2)
                    if (IDS(depth, new Node(Action(current.field, 1, x, y), 1, current.depth + 1, current.path_cost + 1, current), out res, ref N))
                        end = true;
                if (end)
                    return end;
                if (x != 2)
                    if (IDS(depth, new Node(Action(current.field, 2, x, y), 2, current.depth + 1, current.path_cost + 1, current), out res, ref N))
                        end = true;
                if (end)
                    return end;
                if (y != 0)
                    if (IDS(depth, new Node(Action(current.field, 3, x, y), 3, current.depth + 1, current.path_cost + 1, current), out res, ref N))
                        end = true;
            }
            return end;
        }
    }
}
