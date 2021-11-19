using System;
using System.Collections.Generic;

namespace Лаб._3
{
    class Program
    {
        const int N = 300;
        const int M = 30;
        static int[,] edges = new int[N,N];

        static double[] pheromones = new double[N];
        static double[] current_ph = new double[N];
        static List<Ant> ants = new List<Ant>();

        static List<int> T_best = new List<int>();
        static int L_best = 12500;

        static void Main(string[] args)
        {

            Random rand = new Random();
            for (int i = 0; i < N; i++)
            {
                for(int j = 0; j < N; j++)
                {
                    if(i!=j)
                        edges[i, j] = edges[j, i] = rand.Next(1, 61);
                    else
                        edges[i, i] = rand.Next(1, 61);
                }
            }
            for (int i = 0; i < 10; i++)
            {
                ants = new List<Ant>();
                List<int> loc_men = new List<int>();
                for(int h= 0; h < M; h++)
                {
                    int m = rand.Next(0, N);
                    while(loc_men.Contains(m)){
                        m = rand.Next(0, N);
                    }
                    ants.Add(new Ant());
                    ants[h].road.Add(m);
                }
                for (int t = 1; t <= N; t++)
                {
                    for (int k = 0; k < M; k++)
                    {
                        List<double> local_chances = new List<double>();
                        double Loc_sum = 0;
                        double rand_sum = 0;
                        int cur_point = ants[k].road[t - 1];
                        for (int h = 0; h < N; h++)
                        {
                            double loc = Ant.Chance(pheromones[h], edges[cur_point, h]);
                            local_chances.Add(loc);
                            Loc_sum += loc;
                        }
                        for (int h = 0; h < N; h++)
                        {
                            local_chances[h] = Math.Round(local_chances[h] / Loc_sum, 4);
                            if (ants[k].road.Contains(h))
                                local_chances[h] = 0;
                            rand_sum += local_chances[h];
                        }

                        rand_sum = Math.Round(rand_sum, 4);
                        double cur = (double)rand.Next((int)(rand_sum * 10000) + 1) / 10000;
                        Loc_sum = 0;
                        for (int h = 0; h < N; h++)
                        {
                            Loc_sum += local_chances[h];
                            if ((Loc_sum >= cur && local_chances[h] != 0 || Math.Round(Loc_sum,4)>= rand_sum) && !ants[k].road.Contains(h))
                            {
                                ants[k].road.Add(h);
                                ants[k].L += edges[cur_point, h];
                                current_ph[h] += 1 / ants[k].L;
                                h = N;
                            }
                        }
                    }
                }
                for(int h = 0; h < N; h++)
                {
                    pheromones[h] = Ant.Update_ph(pheromones[h], current_ph[h]);
                }
                for (int h = 0; h < M; h++) 
                {
                    if (L_best > ants[h].L)
                    {
                        L_best = ants[h].L;
                        T_best = ants[h].road;
                    }
                }
                int cur_w = 0; 
                for (int h = 1; h < 250; h++)
                {
                    int q = T_best[h - 1];
                    int e = T_best[h];
                    cur_w += edges[q, e];
                    pheromones[e] += 15 / cur_w;
                }
            }
            for(int i = 1; i <= N; i++)
            {
                Console.Write($"\t{T_best[i-1]}");
                if (i % 8 == 0)
                    Console.Write("\n\n");
            }
            Console.WriteLine("\n\nМинимальный путь: " + L_best + "\n\n");
        }

    }
}
