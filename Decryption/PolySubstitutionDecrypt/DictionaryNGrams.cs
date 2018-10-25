using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Decryption.PolySubstitutionDecrypt
{
    class DictionaryNGrams
    {
        private readonly int[] NGrams = { 1, 2, 3, 4, 5, 6 };
        private readonly char[] alphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
        private DictionaryNGrams()
        {
            LoadDictionary();
        }
        private static readonly DictionaryNGrams _dictionaryNGrams = new DictionaryNGrams();

        public static DictionaryNGrams GetDictionaryNGrams()
        {
            return _dictionaryNGrams;
        }

        public Dictionary<string, double> Dictionary { get; set; }

        private void LoadDictionary()
        {
            Dictionary = File.ReadLines("../../../Decryption/PolySubstitutionDecrypt/DictionaryNGrams.txt")
                .Select(line => line.Split(' '))
                .ToDictionary(line => line[0].ToLower(), line => Convert.ToDouble(line[1]));
        }
        private void CreateDictionary()
        {
            Dictionary<string, int> words = File.ReadLines("../../../Decryption/PolySubstitutionDecrypt/WordsFrequency.txt")
               .Select(line => line.Split(' '))
               .ToDictionary(line => line[0].ToLower(), line => Convert.ToInt32(line[1]));

            double total = 0.0;
            int iw = 0;

            Dictionary<string, int> NGramsCount = new Dictionary<string, int>();

            foreach(var pair in words)
            {
                iw++;
                Math.DivRem(iw, 1000, out int result);
                if (result == 0)
                {
                    Console.WriteLine(iw);
                }

                foreach (int i in NGrams)
                {
                    for (int j = 0; j < (pair.Key.Length - i + 1); j++)
                    {
                        string key = pair.Key.Substring(j, i);
                        if (!key.All(c => alphabet.Contains(c)))
                        {
                            continue;
                        }
                        total += pair.Value;
                        if (NGramsCount.ContainsKey(key))
                        {
                            NGramsCount[key] += pair.Value;
                        }
                        else
                        {
                            NGramsCount[key] = pair.Value;
                        }
                    }
                }
            }

            Dictionary = new Dictionary<string, double>();

            foreach (var pair in NGramsCount)
            {
                Dictionary[pair.Key] = pair.Value / total;
            }

            using (StreamWriter sw = new StreamWriter("../../../Decryption/PolySubstitutionDecrypt/DictionaryNGrams.txt"))
            {
                foreach (var pair in Dictionary/*.OrderByDescending(pair => pair.Value)*/)
                {
                    sw.WriteLine(pair.Key + " " + pair.Value);
                }
            }
        }
    }
}
