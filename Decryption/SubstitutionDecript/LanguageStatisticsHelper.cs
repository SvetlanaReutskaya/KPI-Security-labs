using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decryption.SubstitutionDecript
{
    public class LanguageStatisticsHelper
    {
        private LanguageStatisticsHelper()
        {
            BiGramDict = LoadBiGramStatistic();
            UniGramDict = LoadUniGramStatistic();
            TriGramDict = LoadTriGramStatistic();
            QuadGramDict = LoadQuadGramStatistic();
            PentaGramDict = LoadPentaGramStatistic();
        }
        private static readonly LanguageStatisticsHelper _languageStatisticsHelper = new LanguageStatisticsHelper();

        public static LanguageStatisticsHelper GetLanguageStatistics()
        {
            return _languageStatisticsHelper;
        }

        public IDictionary<char, double> UniGramDict { get; set; }
        public IDictionary<string, double> BiGramDict { get; set; }
        public IDictionary<string, double> TriGramDict { get; set; }
        public IDictionary<string, double> QuadGramDict { get; set; }
        public IDictionary<string, double> PentaGramDict { get; set; }

        public IDictionary<char, double> LoadUniGramStatistic()
        {
            double sum = File.ReadLines("../../..//Decryption/SubstitutionDecript/ngrams/unigram.csv")
                .Select(line => line.Split(';')).Sum(line => Convert.ToDouble(line[1]));
            return File.ReadLines("../../../Decryption/SubstitutionDecript/ngrams/unigram.csv")
                .Select(line => line.Split(';'))
                .ToDictionary(line => Convert.ToChar(line[0]), line => Convert.ToDouble(line[1]) / sum);
        }

        public IDictionary<string, double> LoadBiGramStatistic()
        {
            double sum = File.ReadLines("../../..//Decryption/SubstitutionDecript/ngrams/bigram.csv")
                .Select(line => line.Split(',')).Sum(line => Convert.ToDouble(line[1]));
            return File.ReadLines("../../../Decryption/SubstitutionDecript/ngrams/bigram.csv")
                .Select(line => line.Split(','))
                .ToDictionary(line => line[0], line => Convert.ToDouble(line[1]) / sum);
        }

        public IDictionary<string, double> LoadTriGramStatistic()
        {
            double sum = File.ReadLines("../../..//Decryption/SubstitutionDecript/ngrams/trigram.csv")
                .Select(line => line.Split(',')).Sum(line => Convert.ToDouble(line[1]));
            return File.ReadLines("../../../Decryption/SubstitutionDecript/ngrams/trigram.csv")
                .Select(line => line.Split(','))
                .ToDictionary(line => line[0], line => Convert.ToDouble(line[1]) / sum);
        }

        public IDictionary<string, double> LoadQuadGramStatistic()
        {
            double sum = File.ReadLines("../../..//Decryption/SubstitutionDecript/ngrams/quadgram1.csv")
                .Select(line => line.Split(',')).Sum(line => Convert.ToDouble(line[1]));
            return File.ReadLines("../../../Decryption/SubstitutionDecript/ngrams/quadgram1.csv")
                .Select(line => line.Split(','))
                .ToDictionary(line => line[0], line => Convert.ToDouble(line[1]) / sum);
        }

        public IDictionary<string, double> LoadPentaGramStatistic()
        {
            double sum = File.ReadLines("../../..//Decryption/SubstitutionDecript/ngrams/pentagram1.csv")
                .Select(line => line.Split(',')).Sum(line => Convert.ToDouble(line[1]));
            return File.ReadLines("../../../Decryption/SubstitutionDecript/ngrams/pentagram1.csv")
                .Select(line => line.Split(','))
                .ToDictionary(line => line[0], line => Convert.ToDouble(line[1]) / sum);
        }

        public IDictionary<char, double> CreateUniGramStatistic(string text)
        {
            double length = Convert.ToDouble(text.Count());
            Dictionary<char, double> uniGramDict = new Dictionary<char, double>();
            foreach(char c in text)
            {
                if (uniGramDict.ContainsKey(c))
                {
                    uniGramDict[c]++;
                }
                else
                {
                    uniGramDict[c] = 1;
                }
            }

            return uniGramDict.ToDictionary(x => x.Key, x => x.Value / length);
        }

        public IDictionary<string, double> CreateBiGramStatistic(string text)
        {
            double length = text.Length - 1;

            Dictionary<string, double> biGramDict = new Dictionary<string, double>();

            for (int j = 0; j < (text.Length - 1); j++)
            {
                string key = text.Substring(j, 2);
                if (biGramDict.ContainsKey(key))
                {
                    biGramDict[key]++;
                }
                else
                {
                    biGramDict[key] = 1;
                }
            }

            return biGramDict.ToDictionary(x => x.Key, x => x.Value / length);
        }

        public IDictionary<string, double> CreateTriGramStatistic(string text)
        {
            double length = text.Length - 2;

            Dictionary<string, double> triGramDict = new Dictionary<string, double>();

            for (int j = 0; j < (text.Length - 2); j++)
            {
                string key = text.Substring(j, 3);
                if (triGramDict.ContainsKey(key))
                {
                    triGramDict[key]++;
                }
                else
                {
                    triGramDict[key] = 1;
                }
            }

            return triGramDict.ToDictionary(x => x.Key, x => x.Value / length);
        }
        public IDictionary<string, double> CreateQuadGramStatistic(string text)
        {
            double length = text.Length - 3;

            Dictionary<string, double> quadGramDict = new Dictionary<string, double>();

            for (int j = 0; j < (text.Length - 3); j++)
            {
                string key = text.Substring(j, 4);
                if (quadGramDict.ContainsKey(key))
                {
                    quadGramDict[key]++;
                }
                else
                {
                    quadGramDict[key] = 1;
                }
            }

            return quadGramDict.ToDictionary(x => x.Key, x => x.Value / length);
        }

    }
}
