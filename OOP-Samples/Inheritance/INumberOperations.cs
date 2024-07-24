using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    public interface INumberOperations
    {
        bool IsSpecialNumber(int number);
        List<int> FindInRange(int start, int end);
        List<int> FindFirstXNumbers(int count);
    }
}
