using System.Collections.Generic;

namespace Лаб._4
{
    public class Chromosome
    {

        public int price;
        public int weight;

        public List<bool> availList;
        public List<Item> items;

        public Chromosome(List<bool> availList, List<Item> items)
        {
            this.availList = new List<bool>(availList);
            this.items = items;
            Change();
        }
        public void Change()
        {
            int price = 0;
            int weight = 0;
            for (int i = 0; i < availList.Count; i++)
            {
                if (availList[i])
                {
                    price += items[i].price;
                    weight += items[i].weight;
                }
            }
            this.price = price;
            this.weight = weight;
        }
    }
    public class Item
    {
        public int price;
        public int weight;

        public Item(int price, int weight)
        {
            this.price = price;
            this.weight = weight;
        }
    }
}

