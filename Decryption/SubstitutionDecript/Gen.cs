using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decryption.SubstitutionDecript
{
    public class Gen : IComparable<Gen>
    {
        private double? rate;
        private string chromosome;
        private readonly string encryptedText;

        public string Chromosome
        {
            get
            {
                return chromosome;
            }
            set
            {
                rate = null;
                chromosome = value;
            }
        }

        public Gen(string chromosome, string encryptedText)
        {
            rate = null;
            Chromosome = chromosome;
            this.encryptedText = encryptedText;
        }

        public double CalculateFitness()
        {
            if (rate.HasValue)
                return rate.Value;
            else
            {
                SubstitutionDecrypt decriptor = new SubstitutionDecrypt(Chromosome);
                string decryptedText = decriptor.DecryptText(encryptedText);
                rate = CipherFitness.NewEvaluate(decryptedText);
                return rate.Value;
            }
            
        }

        public int CompareTo(Gen other)
        {
            CalculateFitness();
            other.CalculateFitness();
            int result = other.rate.Value.CompareTo(rate.Value);
            if(result != 0)
            {
                return result;
            }
            return Chromosome.CompareTo(other.Chromosome);
        }
    }
}
