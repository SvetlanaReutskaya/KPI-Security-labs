using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Decryption.SubstitutionDecript
{
    public class GeneticModel
    {
        Random rand = new Random();

        private readonly char[] alphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
        private string encryptedText;
        private SortedSet<Gen> population;
        private int populationCount = 30;
        private double probabilityOfMutation = 0.7;
        private int mutationCount = 1;
        private double percentageOfElitism = 10;
        private int currentGeneration;

        public GeneticModel(string encryptedText)
        {         
            this.encryptedText = encryptedText;
            population = new SortedSet<Gen>();
        }

        public string GenerateRandomKey()
        {
            char[] key = alphabet;
            for (int i = 0; i < key.Length; i++)
            {
                char c = key[i];
                int shuffleIndex = rand.Next(key.Length - i) + i;
                key[i] = key[shuffleIndex];
                key[shuffleIndex] = c;
            }
            return new string(key);
        }

        public string Run()
        {
            currentGeneration = 1;
            CreateFirstPopulation();
            double maxRate = 0.0;
            double sumRate = 0.0;
            foreach (var gen in population)
            {
                sumRate += gen.CalculateFitness();
                if (maxRate < gen.CalculateFitness())
                    maxRate = gen.CalculateFitness();
            }

            double maxRate1 = maxRate;
            Console.Clear();
            List<string> output = Decryption.WordNinja.WordNinja.Split(new SubstitutionDecrypt(population.First().Chromosome).DecryptText(encryptedText));
            foreach (var elem in output)
            {
                Console.Write(elem + " ");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("generation: " + currentGeneration + " max rate: " + maxRate +  " avg rate: " + 
                (sumRate / populationCount) + " gen: " + population.First().Chromosome);
            int i = 0;
            int j = 0;
            int k = 0;
            while (true)
            {
                i++;
                if (i == 20)
                {
                    if (CipherFitness.NewEvaluate(new SubstitutionDecrypt(population.First().Chromosome).DecryptText(encryptedText)) < 110)
                        i = 0;
                }

                k++;
                if (k == 20)
                {
                    Console.Clear();
                    output = Decryption.WordNinja.WordNinja.Split(new SubstitutionDecrypt(population.First().Chromosome).DecryptText(encryptedText));
                    foreach (var elem in output)
                    {
                        Console.Write(elem + " ");
                    }
                    Console.WriteLine();
                    Console.WriteLine("\ngeneration: " + currentGeneration + " max rate: " + maxRate + " avg rate: " +
                    (sumRate / populationCount) + " gen: " + population.First().Chromosome);
                    k = 0;
                }
                CreateNewGeneration();

                maxRate = 0.0;
                sumRate = 0.0;
                foreach (var gen in population)
                {
                    sumRate += gen.CalculateFitness();
                    if (maxRate < gen.CalculateFitness())
                        maxRate = gen.CalculateFitness();
                }

                if (maxRate1 + 0.1 > maxRate)
                {
                    j++;
                }
                else j = 0;

                maxRate1 = maxRate;

                if (j == 150)
                {
                    population = new SortedSet<Gen>();
                    CreateFirstPopulation();
                }
                if (maxRate > 105)
                {
                    probabilityOfMutation = 0.25;
                }
                
                if (i == 100)
                {
                    Console.Clear();
                    return new SubstitutionDecrypt(population.First().Chromosome).DecryptText(encryptedText);
                }
            }
        }

        private void CreateFirstPopulation()
        {
            population.Add(new Gen(new string(alphabet), encryptedText));
            while (population.Count < populationCount)
            {
                Gen gen = new Gen(GenerateRandomKey(), encryptedText);
                population.Add(gen);
            }
        }

        private Gen Mutate(Gen gen)
        {
            int length = gen.Chromosome.Length;
            char[] chromosome = gen.Chromosome.ToCharArray();
            for (int i = 0; i < mutationCount; i++)
            {
                int oldPosition = rand.Next(0, length);
                int newPosition = -1;
                do
                {
                    newPosition = rand.Next(0, length);
                }
                while (oldPosition == newPosition);
                char oldChar = chromosome[oldPosition];
                chromosome[oldPosition] = chromosome[newPosition];
                chromosome[newPosition] = oldChar;
            }
           
            while(rand.NextDouble() < probabilityOfMutation)
            {
                int oldPosition = rand.Next(0, length);
                int newPosition = -1;
                do
                {
                    newPosition = rand.Next(0, length);
                }
                while (oldPosition == newPosition);
                char oldChar = chromosome[oldPosition];
                chromosome[oldPosition] = chromosome[newPosition];
                chromosome[newPosition] = oldChar;
            }
            return new Gen(new string(chromosome), encryptedText);
        }

        public void CreateNewGeneration()
        {
            SortedSet<Gen> newPopulation = new SortedSet<Gen>();
            List<Gen> populationArray = population.ToList();
            int elitismAmount = (int)Math.Ceiling(populationCount * (percentageOfElitism / 100.0));
            
            int numberOfChildren = populationCount - elitismAmount;
            if (elitismAmount > 0)
            {
                for (int i = 0; i < elitismAmount; i++)
                {
                    newPopulation.Add(populationArray[i]);
                }
            }
            if (numberOfChildren > 0)
            {
                double sumRate = 0.0;
                for (int i = 0; i < elitismAmount; i++)
                {
                    sumRate += populationArray[i].CalculateFitness();
                }
                List<int> index = new List<int>();
                for (int i = 0; i < elitismAmount; i++)
                {
                    int count = (int)Math.Ceiling(Math.Pow(populationArray[i].CalculateFitness() / 100.0, 1.5));
                    for (int j = 0; j < count; j++)
                    {
                        index.Add(i);
                    }
                }

                while (newPopulation.Count < populationCount)
                {
                    newPopulation.Add(Mutate(populationArray[index[rand.Next(0, index.Count)]]));
                }
            }
            
            population = newPopulation;
            currentGeneration++;
        }

    }
}
