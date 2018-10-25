using System;
using System.Collections.Generic;
using System.Linq;

namespace Decryption.SubstitutionDecript
{
    class CipherFitness
    {
        private static readonly int[] NGrams = { 1, 2, 3, 4, 5, 6 };
 
        public static double NewEvaluate(string decryptedText)
        {
            decryptedText = decryptedText.ToLower();

            var statisticHelper = LanguageStatisticsHelper.GetLanguageStatistics();
            var uniDict = statisticHelper.UniGramDict;
            var biDict = statisticHelper.BiGramDict;
            var triDict = statisticHelper.TriGramDict;
            var quadDict = statisticHelper.QuadGramDict;
            var pentaDict = statisticHelper.PentaGramDict;

            double uni = 0.0;
            double bi = 0.0;
            double tri = 0.0;
            double quad = 0.0;
            double penta = 0.0;

            foreach (char c in decryptedText)
            {
                uniDict.TryGetValue(c, out double prob);
                uni += prob;
            }

            for (int j = 0; j < (decryptedText.Length - 1); j++)
            {
                string key = decryptedText.Substring(j, 2);
                biDict.TryGetValue(key, out double prob);
                bi += prob * 4;
            }

            for (int j = 0; j < (decryptedText.Length - 2); j++)
            {
                string key = decryptedText.Substring(j, 3);
                triDict.TryGetValue(key, out double prob);
                tri += prob * 9;
            }

            for (int j = 0; j < (decryptedText.Length - 3); j++)
            {
                string key = decryptedText.Substring(j, 4);
                quadDict.TryGetValue(key, out double prob);
                quad += prob * 16;
            }
            for (int j = 0; j < (decryptedText.Length - 4); j++)
            {
                string key = decryptedText.Substring(j, 5);
                pentaDict.TryGetValue(key, out double prob);
                penta += prob * 25;
            }

            return (uni + bi + tri + quad + penta) / decryptedText.Length * 1000;
        }
    }
}
