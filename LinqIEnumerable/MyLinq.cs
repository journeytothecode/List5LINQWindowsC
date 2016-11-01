using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqIEnumerable
{
    public static class MyLinq
    {
        // extension methods
        public static int MyCount<T>(this IEnumerable<T> sequence) // Generic type of T// invorke by 'this'
        {
            int c = 0;
            foreach (var item in sequence)
            {
                c += 1;
            }
            return c;
        }
    }
}
