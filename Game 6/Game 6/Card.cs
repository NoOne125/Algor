using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_6
{
    class Card
    {
        public int rank;
        public int suit;

        public Card(int a, int b)
        {
            rank = a;
            suit = b;
        }

        public Card() { }

        public string Card_Get()
        {
            string text = "";
            switch (suit)
            {
                case 0:
                    text += "♥";
                    break;
                case 1:
                    text += "♠";
                    break;
                case 2:
                    text += "♦";
                    break;
                case 3:
                    text += "♣";
                    break;
            }
            text += " ";
            switch (rank)
            {
                case 11:
                    text += "В";
                    break;
                case 12:
                    text += "Д";
                    break;
                case 13:
                    text += "К";
                    break;
                case 14:
                    text += "Т";
                    break;
                default:
                    text += Convert.ToString(rank);
                    break;
            }
            return text;
        }

    }
}
