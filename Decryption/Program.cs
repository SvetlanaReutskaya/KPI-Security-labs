using System;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using Decryption.SubstitutionDecript;
using Decryption.PolySubstitutionDecrypt;

namespace Decryption
{
    class Program
    {
        #region To Do List
        
        // To DO List:

        // 1. Maybe needed to refactor code

        // 2. Decrypt string 6 by some way

        #endregion

        static void Main(string[] args)
        {

            #region Task2

            CeasarDecrypt ceasarDecrypt = new CeasarDecrypt
                (File.ReadAllText("../../../Decryption/SubstitutionDecript/tasks/task2.txt"));
            string result2 = ceasarDecrypt.Decrypt();
            Console.WriteLine("Ceasar Decrypt:\n" + result2 + "\n");

            #endregion

            #region Task3

            VigenereDecrypt vigenereDecrypt = new VigenereDecrypt(DecodeHexToUTF8
                (File.ReadAllText("../../../Decryption/SubstitutionDecript/tasks/task3.txt")), null);
            string result3 = vigenereDecrypt.Decrypt();
            Console.WriteLine("Vigenere Decrypt:\n" + result3 + "\n");
            Console.WriteLine("Press any key to start mono substitution decrypt");
            Console.ReadKey();

            #endregion

            #region Task4

            Console.Clear();
            Console.WriteLine("Initializing dictionaries...");

            Stopwatch task4Stopwatch = new Stopwatch();
            task4Stopwatch.Start();

            string task4 = File.ReadAllText("../../../Decryption/SubstitutionDecript/tasks/task4.txt").ToLower();
            
            GeneticModel gm = new GeneticModel(task4);
            string result4 = gm.Run();
            task4Stopwatch.Stop();

            Console.WriteLine("Result:\n");
            List<string> output = Decryption.WordNinja.WordNinja.Split(result4);
            foreach (var elem in output)
            {
                Console.Write(elem + " ");
            }

            Console.WriteLine("\n");
            Console.WriteLine("Execution time: " + (task4Stopwatch.ElapsedMilliseconds / 1000.0).ToString("F3") + " seconds\n");
            

            Console.WriteLine("Press any key to start poly substitution decrypt");
            Console.ReadKey();
            #endregion

            #region Task5

            Console.Clear();
            Console.WriteLine("Initializing dictionaries...");

            Stopwatch task5Stopwatch = new Stopwatch();
            task5Stopwatch.Start();

            int[] ngrams = { 2, 3, 4 };
            var dict = DictionaryNGrams.GetDictionaryNGrams().Dictionary;
            var words = WordDictionary.GetWordDictionary().Dictionary;
            var smallWords = WordDictionary.GetWordDictionary().SmallDictionary;
            string task5 = File.ReadAllText("../../../Decryption/SubstitutionDecript/tasks/task5.txt").ToLower();

            string result5 = NewGeneticAlgo.GeneticAlgo(task5, 6, ngrams, dict, words, smallWords);

            task5Stopwatch.Stop();

            Console.WriteLine(result5);

            Console.WriteLine("Execution time: " + task5Stopwatch.Elapsed.Minutes + " minutes " + task5Stopwatch.Elapsed.Seconds + " seconds");
            #endregion
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public static string DecodeHexToUTF8(string task)
        {
            string result = string.Empty;
            for (int i = 0; i < task.Length; i += 2)
            {
                result += (char)int.Parse(task.Substring(i, 2), System.Globalization.NumberStyles.HexNumber);
            }
            return result;
        }
    }

}
