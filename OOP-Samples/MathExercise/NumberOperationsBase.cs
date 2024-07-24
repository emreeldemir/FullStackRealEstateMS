using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathExercise
{
    internal abstract class NumberOperationsBase : INumberOperations
    {
        public abstract bool IsSpecialNumber(int number);
        public List<int> FindFirstXNumbers(int count)
        {
            List<int> resultList = new List<int>();
            int number = 1;
            while (resultList.Count < count)
            {
                if (IsSpecialNumber(number))
                {
                    resultList.Add(number);
                }
                number++;
            }
            return resultList;
        }

        public List<int> FindInRange(int start, int end)
        {
            List<int> resultList = new List<int>();
            for (int i = start; i <= end; i++)
            {
                if (IsSpecialNumber(i))
                {
                    resultList.Add(i);
                }
            }
            return resultList;
        }

        //public static INumberOperations Factory(int number)
        //{
        //    if (number == 1)
        //    {
        //        return new PrimeNumberOperations();
        //    }
        //    else if (number == 2)
        //    {
        //        return new PerfectNumberOperations();
        //    }
        //    else
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

    }
}
