using System;

namespace Decryption
{
    internal sealed class CeasarDecrypt
    {
        public string source;

        public CeasarDecrypt(string source)
        {
            this.source = source;
        }

        //key 19

        public string Decrypt()
        {
            string result = string.Empty;

            for (int j = 0; j < 256; j++)
            {
                string text = string.Empty;

                foreach (char c in source)
                {
                    text += (char)(c ^ j);
                }

                int letterCount = 0;
                int spaceCount = 0;

                foreach (char c in text)
                {
                    if (Char.IsLetter(c) || c == '=')
                        letterCount++;
                    if (Char.IsWhiteSpace(c))
                        spaceCount++;
                }

                if ((((double)letterCount / text.Length) > 0.7) 
                    && (((double)spaceCount / text.Length) > 0.12) 
                    && (((double)(letterCount + spaceCount) / text.Length) > 0.9))
                {
                    result += "key: " + (char)j + "\nresult string:\n" + text;
                    break;
                }        
            }
            return result;
        }
    }
}
