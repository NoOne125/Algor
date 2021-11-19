using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаб._1
{
    public class Node
    {
        public sbyte[,] field;
        public int action;
        public int depth;
        public int path_cost;
        public Node parent;
        public Node(sbyte[,] field, int action, int depth, int path_cost, Node parent)
        {
            this.field = field;
            this.action = action;
            this.depth = depth;
            this.path_cost = path_cost;
            this.parent = parent;
        }
        public Node() { }
    }
}
