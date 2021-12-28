using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_6
{
    class Belt
    {

        private int N_gr;
        public int N;
        private List<Card> deck;
        public readonly Card trump;
        private List<Card> hand_1;
        private List<Card> hand_2;

        public List<int> opponent_comb;
        public List<int> gamer_comb;

        private List<List<Card>> field;

        public Belt(int N_gr)
        {
            this.N_gr = N_gr;
            N = 52;

            hand_1 = new List<Card>();
            hand_2 = new List<Card>();
            opponent_comb = new List<int>();
            gamer_comb = new List<int>();
            field = new List<List<Card>>();

            //Создание колоды
            deck = new List<Card>();
            List<Card> local = new List<Card>();
            for (int i = 2; i <= 14; i++)
            {
                field.Add(new List<Card>());
                for (int j = 0; j < 4; j++)
                    local.Add(new Card(i, j));
            }
            Random rand = new Random();
            while (local.Count > 0)
            {
                int card_num = rand.Next(local.Count);
                deck.Add(local[card_num]);
                local.RemoveAt(card_num);
            }
            trump = deck[0];
            deck.RemoveAt(0);
        }

        public List<Card> Get_Hand()
        {
            return hand_1;
        }

        public List<List<Card>> Get_Field()
        {
            List<List<Card>> Local = new List<List<Card>>();
            foreach(var k in field)
            {
                Local.Add(new List<Card>());
                foreach(var n in k)
                {
                    Local[Local.Count - 1].Add(n);
                }
            }
            return Local;
        }

        public void Full_Hand(out int N_2, out List<Card> Get_1)
        {
            int i = 0;
            N_2 = 0;
            Get_1 = new List<Card>();
            if (N != 0)
                while (hand_1.Count != 4 || hand_2.Count != 4)
                {
                    if (hand_1.Count != 4 && i % 2 == 0)
                    {
                        if (N == 1)
                        {
                            hand_1.Add(trump);
                            Get_1.Add(trump);
                            N--;
                            return;
                        }
                        hand_1.Add(deck[0]);
                        Get_1.Add(deck[0]);
                        deck.RemoveAt(0);
                        N--;
                    }
                    else if(hand_2.Count != 4)
                    {
                        if (N == 1)
                        {
                            hand_1.Add(trump);
                            N--;
                            return;
                        }
                        hand_2.Add(deck[0]);
                        deck.RemoveAt(0);
                        N_2++;
                        N--;
                    }
                    i++;
                }
        }

        public int Turn(bool[] k, bool gamer)
        {
            if (N == 0 && (gamer && hand_2.Count == 0 || !gamer && hand_1.Count == 0))
                k = new bool[4] { true, true, true, true };
            if (gamer)
            {
                for (int i = hand_1.Count - 1; i >= 0; i--)
                {
                    if (k[i])
                    {
                        Card card = hand_1[i];
                        field[card.rank - 2].Add(card);
                        hand_1.RemoveAt(i);
                    }
                }
                if (gamer_comb.Count == 0)
                    for (int i = 12; i >= 0; i--)
                    {
                        if (field[i].Count >= N_gr)
                        {
                            gamer_comb.Add(i + 2);
                            for (int j = 0; j < N_gr; j++)
                                field[i].RemoveAt(0);
                            break;
                        }
                    }
                if (gamer_comb.Count != 0)
                    for (int i = 0; i < gamer_comb.Max() - 2; i++)
                    {
                        if (field[i].Count >= N_gr)
                        {
                            gamer_comb.Add(i);
                            for (int j = 0; j < N_gr; j++)
                                field[i].RemoveAt(0);
                        }
                    }
            }
            else
            {
                for (int i = hand_2.Count - 1; i >= 0; i--)
                {
                    if (k[i])
                    {
                        Card card = hand_2[i];
                        field[card.rank - 2].Add(card);
                        hand_2.RemoveAt(i);
                    }
                }
                if (opponent_comb.Count == 0)
                    for (int i = 12; i >= 0; i--)
                    {
                        if (field[i].Count >= N_gr)
                        {
                            opponent_comb.Add(i + 2);
                            for (int j = 0; j < N_gr; j++)
                                field[i].RemoveAt(0);
                            break;
                        }
                    }
                if (opponent_comb.Count != 0)
                    for (int i = 0; i < opponent_comb.Max() - 2; i++)
                    {
                        if (field[i].Count >= N_gr)
                        {
                            opponent_comb.Add(i);
                            for (int j = 0; j < N_gr; j++)
                                field[i].RemoveAt(0);
                        }
                    }
            }
            if (gamer)
                return gamer_comb.Count;
            else
                return opponent_comb.Count;
        }

        public bool[] Bot()
        {
            bool[] turn = new bool[4];
            List<List<Card>> local = Get_Field();
            List<int> l = new List<int>();
            List<int> l_2 = new List<int>();
            for (int i = 0; i < 4; i++)
            {
                l.Add(hand_2[i].rank - 2);
                l_2.Add(hand_2[i].rank - 2);
            }
            while (l.Count > 0)
            {
                int k = l[0];
                local[k].Add(trump);
                l.RemoveAt(0);
                while (l.Contains(k))
                {
                    local[k].Add(trump);
                    for (int i = l.Count - 1; i >= 0; i--) 
                    {
                        if (l[i] == k)
                        {
                            l.RemoveAt(i);
                            break;
                        }
                    }
                }
            }
            if (opponent_comb.Count == 0)
            {
                bool find = false;
                for(int i = 12; i >=8; i--)
                {
                    if (local[i].Count >= N_gr)
                    {
                        for(int j=0; j < 4; j++)
                        {
                            if (l_2[j] == i)
                            {
                                turn[j] = true;
                                find = true;
                            }
                        }
                    }

                }
                if (find)
                {
                    for (int i = 7; i >= 0; i--)
                    {
                        if (local[i].Count >= N_gr)
                        {
                            for (int j = 0; j < 4; j++)
                            {
                                if (l_2[j] == i)
                                {
                                    turn[j] = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 7; i >= 0; i--)
                    {
                        if (local[i].Count < N_gr)
                        {
                            for (int j = 0; j < 4; j++)
                            {
                                if (l_2[j] == i)
                                {
                                    turn[j] = true;
                                }
                            }
                        }
                    }
                    if(!(turn[0] || turn[1] || turn[2] || turn[3])){
                        int max = l_2.Max();
                        for(int i = 0; i < 4; i++)
                        {
                            turn[i] = l_2[i] == max;
                            return turn;
                        }
                    }
                }
            }
            else
            {
                for (int i = 12; i >= 0; i--)
                {
                    if (local[i].Count >= N_gr)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (l_2[j] == i)
                            {
                                turn[j] = true;
                            }
                        }
                    }
                }
                if (!(turn[0] || turn[1] || turn[2] || turn[3]))
                {
                    int max = l_2.Max();
                    for (int i = 0; i < 4; i++)
                    {
                        turn[i] = l_2[i] == max;
                        return turn;
                    }
                }
            }
            return turn;
        }
    }
}
