using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Лаб._1.Other;

namespace Лаб._1
{
    class RBFS_m
    {
        public static Node_rbfs RBFS(Node_rbfs current, ref int f_limit, ref int N)
        {
            N++;
            if (current.f == 9)
                return current;
            else if (current.f < f_limit)
            {
                return null;
            }
            List<Node_rbfs> sus = new List<Node_rbfs>();
            int x = -1, y = -1;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (current.field[i, j] == 0)
                    {
                        x = i;
                        y = j;
                    }
            if (x != 0 && current.action!=2)
            {
                sus.Add(new Node_rbfs(Action(current.field, 0, x, y), 0, current));
            }
            if (y != 2 && current.action != 3)
            {
                sus.Add(new Node_rbfs(Action(current.field, 1, x, y), 1, current));
            }
            if (x != 2 && current.action != 0)
            {
                sus.Add(new Node_rbfs(Action(current.field, 2, x, y), 2, current));
            }
            if (y != 0 && current.action != 1)
            {
                sus.Add(new Node_rbfs(Action(current.field, 3, x, y), 3, current));
            }
            int f_l = f_limit;
            sus = sus.OrderBy(z => z.f).ToList();
            sus.Reverse();
            if (sus.Count == 0)
                return null;
            if (sus.Count == 1)
            {
                return RBFS(sus[0], ref f_limit, ref N);
            }
            else
            {
                for (int i = 0; i < sus.Count; i++)
                {
                    if(i==sus.Count-1)
                        return RBFS(sus[i], ref f_limit, ref N);
                    f_limit = Math.Max(f_l, sus[i+1].f);
                    Node_rbfs v = RBFS(sus[i], ref f_limit, ref N);
                    if (v != null)
                        return v;
                }
            }
            return null;
        }
    }
}
