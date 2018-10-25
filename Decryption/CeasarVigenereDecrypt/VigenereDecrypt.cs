using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decryption
{
    public class VigenereDecrypt
    {
        public string source;
        public int keyLength;
        public int[] key;

        public VigenereDecrypt(string source, int? keyLength)
        {
            this.source = source;
            this.keyLength = keyLength!=null? Convert.ToInt32(keyLength): FindKeyLength();
            key = new int[this.keyLength];
        }

        private int FindKeyLength()
        {
            int maxIndex = -1;
            double indexOfConcidence = -1;
            double curIndex;

            for (int i = 1; i < 50; i++)
            {
                LetterCounter letterCounter = new LetterCounter(source, i);
                curIndex = letterCounter.IndexOfConcidence();
                if (curIndex > indexOfConcidence)
                {
                    if(curIndex/ indexOfConcidence > 1.4)
                    {
                        maxIndex = i;
                        return maxIndex;
                    }
                    indexOfConcidence = letterCounter.IndexOfConcidence();
                    maxIndex = i;
                }
            }
            return maxIndex;
        }

        public string Decrypt()
        {
            key = new int[keyLength];
            string result = string.Empty;

            for (int i = 0; i < keyLength; i++)
            {
                for (int j = 0; j < 256; j++)
                {
                    result = string.Empty;

                    for (int k = 0; k < source.Length - keyLength + 1; k += keyLength)
                    {
                        result += Convert.ToChar(source[k + i] ^ j);
                    }

                    int letterCount = 0;
                    int spaceCount = 0;

                    foreach (char c in result)
                    {
                        if (Char.IsLetter(c))
                            letterCount++;
                        if (Char.IsWhiteSpace(c))
                            spaceCount++;
                    }

                    if ((((double)letterCount / result.Length) > 0.7)
                        && (((double)spaceCount / result.Length) > 0.12)
                        && (((double)(letterCount + spaceCount) / result.Length) > 0.95))
                    {
                        key[i] = j;
                        break;
                    }
                }
            }

            result = string.Empty;

            result += "key: ";
            for (int i = 0; i < keyLength; i++)
            {
                result += Convert.ToChar(key[i]);
            }

            result += "\nresult string:\n";
            for (int i = 0; i < source.Length - keyLength + 1; i += keyLength)
            {
                for (int j = 0; j < keyLength; j++)
                {
                    result += Convert.ToChar(source[i + j] ^ key[j]);
                }
            }

            return result;
        }
    }
}
