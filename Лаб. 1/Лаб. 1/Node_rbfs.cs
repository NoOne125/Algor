using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаб._1
{
    class Node_rbfs
    {
        public sbyte[,] field;
        public int action;
        public int f;
        public Node_rbfs parent;
        public Node_rbfs(sbyte[,] field, int action, Node_rbfs parent)
        {
            this.field = field;
            this.action = action;
            this.parent = parent;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (field[i, j] == Other.field_good[i, j])
                        f++;
        }
        public Node_rbfs() { }
    }
}
