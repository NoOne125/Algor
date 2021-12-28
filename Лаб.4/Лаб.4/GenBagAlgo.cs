using System;
using System.Collections.Generic;
using System.Linq;

namespace Лаб._4
{
    public class GenBagAlgo
    {
        private int full_w;
        private int N_items;
        public List<Item> items;

        public GenBagAlgo(int full_w, int N_items, List<Item> items)
        {
            this.full_w = full_w;
            this.N_items = N_items;
            this.items = items;
        }

        public Chromosome GetBest(int iterations)
        {
            List<Chromosome> population = GenPopulation();

            for (int i = 0; i < iterations; i++)
            {
                population = LocalImprovementOperator(population);
                population = SelectNewPopulation(population);
                population = Crossingover(population);
                Mutate(population);

                if (i % 20 == 0)
                {
                    Chromosome tempbest = FindBest(population);
                    int num = i + 1;
                    Console.WriteLine("Итерация: "+ num + "; Цена: " + tempbest.price + "; Вес: " + tempbest.weight + "");
                }

            }
            Chromosome best = FindBest(population);
            return best;
        }

        List<Chromosome> LocalImprovementOperator(List<Chromosome> population)
        {
            List<Chromosome> newPopulation = new List<Chromosome>();
            foreach (Chromosome chrom in population)
            {
                int maxweightIndex = 0;
                int maxweight = 0;
                int maxpriceIndex = 0;
                int maxprice = 0;
                Chromosome chromosome = new Chromosome(chrom.availList, items);
                for (int i = 0; i < chromosome.availList.Count(); i++)
                {
                    bool isInBag = chromosome.availList[i];
                    int weight = items[i].weight;
                    int price = items[i].price;
                    if (isInBag && weight > maxweight)
                    {
                        maxweight = weight;
                        maxweightIndex = i;
                    }
                    if (!isInBag && price > maxprice)
                    {
                        maxprice = price;
                        maxpriceIndex = i;
                    }
                }
                chromosome.availList[maxweightIndex] = false;
                chromosome.availList[maxpriceIndex] = true;
                chromosome.Change();
                if (chromosome.weight > this.full_w || chromosome.price < chrom.price)
                    newPopulation.Add(chrom);
                else
                    newPopulation.Add(chromosome);
            }
            return newPopulation;
        }

        List<Chromosome> SelectNewPopulation(List<Chromosome> population)
        {
            List<Chromosome> newPopulation = new List<Chromosome>();
            Random rnd = new Random();

            for (int i = 0; i < population.Count(); i++)
            {
                int index1 = (int)(rnd.NextDouble() * population.Count());
                int index2 = (int)(rnd.NextDouble() * population.Count());
                int index3 = (int)(rnd.NextDouble() * population.Count());

                while (index1 == index2 || index2 == index3 || index1 == index3)
                {
                    index2 = (int)(rnd.NextDouble() * population.Count());
                    index3 = (int)(rnd.NextDouble() * population.Count());
                }

                Chromosome bestChromosome = population[index1];

                if (bestChromosome.price < population[index2].price)
                    bestChromosome = population[index2];

                if (bestChromosome.price < population[index3].price)
                    bestChromosome = population[index3];

                newPopulation.Add(new Chromosome(bestChromosome.availList, items));
            }
            return newPopulation;
        }

        List<Chromosome> Crossingover(List<Chromosome> population)
        {
            List<Chromosome> newPopulation = new List<Chromosome>();

            for (int i = 0; i < population.Count(); i += 2)
            {
                List<bool> availList1 = new List<bool>();
                List<bool> availList2 = new List<bool>();
                List<bool> index1 = population[i].availList;
                List<bool> index2 = population[i + 1].availList;

                for (int j = 0; j < items.Count; j++)
                {
                    if (j < items.Count * 0.25 || (j > items.Count * 0.5 && j < items.Count * 0.75))
                    {
                        availList1.Add(index1[j]);
                        availList2.Add(index2[j]);
                    }
                    else
                    {
                        availList1.Add(index2[j]);
                        availList2.Add(index1[j]);
                    }
                }
                Chromosome newChrom1 = new Chromosome(availList1, items);
                Chromosome newChrom2 = new Chromosome(availList2, items);

                if (newChrom1.weight > full_w)
                    newChrom1 = new Chromosome(index1, items);
                if (newChrom2.weight > full_w)
                    newChrom2 = new Chromosome(index2, items);

                newPopulation.Add(newChrom1);
                newPopulation.Add(newChrom2);
            }
            return newPopulation;
        }

        void Mutate(List<Chromosome> population)
        {
            Random rnd = new Random();

            foreach (Chromosome chrom in population)
            {
                if (rnd.NextDouble() < 0.05)
                {
                    int ind = (int)(rnd.NextDouble() * items.Count);

                    chrom.availList[ind] = !chrom.availList[ind];
                    chrom.Change();
                    if (chrom.weight > full_w)
                    {
                        chrom.availList[ind] = !chrom.availList[ind];
                        chrom.Change();
                    }
                }
            }
        }

        Chromosome FindBest(List<Chromosome> population)
        {
            Chromosome bestChromosome = population[0];
            for (int i = 1; i < population.Count(); i++)
            {
                if (bestChromosome.price < population[i].price)
                    bestChromosome = population[i];
            }
            return bestChromosome;
        }

        List<Chromosome> GenPopulation()
        {
            Random rnd = new Random();

            List<Chromosome> population = new List<Chromosome>();
            List<bool> availList = new List<bool>();

            for (int i = 0; i < items.Count; i++)
                availList.Add(false);

            for (int i = 0; i < N_items; i++)
            {
                int pos = (int)(rnd.NextDouble() * items.Count);
                availList[pos] = true;
                Chromosome ind = new Chromosome(availList, items);
                population.Add(ind);
                availList[pos] = false;
            }
            return population;
        }
    }
}
