using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    public class PrimeNumberOperations : INumberOperations
    {
        public bool IsSpecialNumber(int number)
        {
            if (number < 2) return false;
            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0) return false;
            }
            return true;
        }

        public List<int> FindInRange(int start, int end)
        {
            List<int> primes = new List<int>();
            for (int i = start; i <= end; i++)
            {
                if (IsSpecialNumber(i))
                {
                    primes.Add(i);
                }
            }
            return primes;
        }

        public List<int> FindFirstXNumbers(int count)
        {
            List<int> primes = new List<int>();
            int number = 2;
            while (primes.Count < count)
            {
                if (IsSpecialNumber(number))
                {
                    primes.Add(number);
                }
                number++;
            }
            return primes;
        }
    }
}
