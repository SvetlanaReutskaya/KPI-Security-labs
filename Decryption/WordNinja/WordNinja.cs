using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Decryption.WordNinja
{
    public class WordNinja
    {
        static Dictionary<string, double> WordCost { get; }
        static int MaxWord { get; }

        static WordNinja()
        {
            string[] lines = File.ReadAllLines("../../../Decryption/WordNinja/wordninja_words.txt");
            MaxWord = -1;
            WordCost = new Dictionary<string, double>();
            for (int i = 0; i < lines.Length; i++)
            {
                WordCost[lines[i]] = Math.Log((i + 1) * Math.Log(lines.Length));

                if (lines[i].Length > MaxWord)
                {
                    MaxWord = lines[i].Length;
                }
            }
            //Console.WriteLine(MaxWord);
            //Console.WriteLine(WordCost["on"]);
            //foreach (var elem in WordCost)
            //{
            //    Console.Write(elem.Key + " " + elem.Value + "; ");
            //}
        }

        public static List<string> Split(string s)
        {
            s = s.ToLower();
            List<double> cost = new List<double>();
            cost.Add(0);
            Tuple<double, int> BestMatch(int i)
            {
                List<double> candidates = new List<double>();
                candidates = cost.GetRange(Math.Max(0, i - MaxWord), i - Math.Max(0, i - MaxWord));
                candidates.Reverse();
                double min = double.MaxValue;
                int pos = 0;
                for (int j = 0; j < candidates.Count; j++)
                {
                    string st = s.Substring(i - j - 1, j + 1);
                    if (!WordCost.TryGetValue(st, out double wordCost))
                        wordCost = float.MaxValue;
                    if (candidates[j] + wordCost < min)
                    {
                        pos = j;
                        min = candidates[j] + wordCost;
                    }
                }
                if (!WordCost.TryGetValue(s.Substring(i - pos - 1, pos + 1), out double wordCost1))
                    wordCost1 = float.MaxValue;

                return new Tuple<double, int>(candidates[pos] + wordCost1, pos + 1);
            }

            for (int i = 1; i <= s.Length; i++)
            {
                Tuple<double, int> c = BestMatch(i);
                cost.Add(c.Item1);
            }

            List<string> result = new List<string>();
            int l = s.Length;
            while (l > 0)
            {
                Tuple<double, int> c = BestMatch(l);
                if (c.Item1 != cost[l])
                    throw new Exception();
                bool newToken = true;
                if (s.Substring(l - c.Item2, c.Item2) != "'")
                {
                    if (result.Count > 0)
                    {
                        if (result[result.Count - 1] == "'s" || (char.IsDigit(s[l - 1]) && char.IsDigit(result[result.Count - 1][0])))
                        {
                            result[result.Count - 1] = s.Substring(l - c.Item2, c.Item2) + result[result.Count - 1];
                            newToken = false;
                        }
                    }
                }

                if(newToken)
                {
                    result.Add(s.Substring(l - c.Item2, c.Item2));
                }

                l -= c.Item2;
            }
            result.Reverse();
            return result;
        }

        //min((c + _wordcost.get(s[i - k - 1:i].lower(), 9e999), k+1)
    }
}
