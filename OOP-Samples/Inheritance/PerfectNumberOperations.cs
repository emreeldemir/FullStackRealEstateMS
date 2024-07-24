using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    internal class PerfectNumberOperations : INumberOperations
    {
        public bool IsSpecialNumber(int number)
        {
            int sum = 0;
            for (int i = 1; i < number; i++)
            {
                if (number % i == 0)
                {
                    sum += i;
                }
            }
            return sum == number;
        }

        public List<int> FindInRange(int start, int end)
        {
            List<int> perfects = new List<int>();
            for (int i = start; i <= end; i++)
            {
                if (IsSpecialNumber(i))
                {
                    perfects.Add(i);
                }
            }
            return perfects;
        }

        public List<int> FindFirstXNumbers(int count)
        {
            List<int> perfects = new List<int>();
            int number = 1;
            while (perfects.Count < count)
            {
                if (IsSpecialNumber(number))
                {
                    perfects.Add(number);
                }
                number++;
            }
            return perfects;
        }
    }
}
