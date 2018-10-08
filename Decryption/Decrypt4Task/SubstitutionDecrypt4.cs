﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decryption.SubstitutionDecript
{
    public class SubstitutionDecrypt4
    {
        private readonly char[] alphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
        public List<string> key;
        public Dictionary<char, char> dictionary;

        public SubstitutionDecrypt4(List<string> key)
        {
            dictionary = new Dictionary<char, char>();
            this.key = key;
            for (int i = 0; i < alphabet.Length; i++) 
            {
                dictionary.Add(key[i][0], alphabet[i]);
            }
        }

        public string DecryptText(string source)
        {
            string decryptedText = "";
            foreach (char c in source)
            {
                if (Char.IsLetter(c))
                {
                    decryptedText += dictionary[c];
                }
                else
                {
                    decryptedText += c;
                }
            }
            return decryptedText;
        }
    }
}
