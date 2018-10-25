using System;
using System.Collections.Generic;

namespace Decryption
{
    internal sealed class Node
    {
        public char Letter { get; set; }
        public int Count { get; set; }

        public Node() { }
        public Node(char c)
        {
            Letter = c;
            Count = 1;
        }
    }

    internal sealed class LetterCounter
    {
        private List<Node> list;
        private int count;

        public LetterCounter()
        {
            list = new List<Node>();
        }

        public LetterCounter(string source, int keyLength)
        {
            list = new List<Node>();
            AddString(source, keyLength);
        }

        public void Add(char c)
        {
            foreach (Node node in list)
            {
                if (node.Letter == c)
                {
                    count++;
                    node.Count++;
                    return;
                }
            }
            list.Add(new Node(c));
            count++;
        }

        public void AddString(string source, int keyLength)
        {
            for (int i = 0; i < source.Length; i+= keyLength)
            {
                Add(source[i]);
            }
        }

        public double IndexOfConcidence()
        {
            double result = 0;
            foreach (Node node in list)
            {
                result += (double) ((node.Count) * (node.Count - 1)) / count / (count-1);
            }
            return result;
        }
        public void Show()
        {
            //Console.WriteLine(list.Count);
            foreach (Node node in list)
            {
                Console.WriteLine(node.Letter + " " + node.Count);
            }
        }
    }
}
