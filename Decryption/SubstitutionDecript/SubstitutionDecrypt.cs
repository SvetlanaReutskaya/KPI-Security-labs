using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decryption.SubstitutionDecript
{
    public class SubstitutionDecrypt
    {
        private readonly char[] alphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
        public string key;
        public Dictionary<char, char> dictionary;

        public SubstitutionDecrypt(string key)
        {
            dictionary = new Dictionary<char, char>();
            this.key = key;
            for (int i = 0; i < alphabet.Length; i++) 
            {
                dictionary.Add(key[i], alphabet[i]);
            }
        }

        public string DecryptText(string source)
        {
            List<char> decryptedText = new List<char>();
            foreach (char c in source)
            {
                if (dictionary.ContainsKey(c))
                {
                    decryptedText.Add(dictionary[c]);
                }
            }
            return new string(decryptedText.ToArray());
        }
    }
}
