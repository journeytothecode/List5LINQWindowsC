using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqIEnumerable
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<Employees> developers = new Employees[]
            { //collections
                new Employees {Id = 1, Name = "Tom" },
                new Employees {Id = 2, Name = "Cruise" }
            };

            IEnumerable<Employees> sales = new List<Employees>()
            {
                new Employees {Id = 3, Name= "Sale man" }
            };

            Console.WriteLine("My own Linq count (custom Linq count {0} developers)",developers.MyCount());
            Console.WriteLine("LinQ count {0} saleman",sales.Count());

            IEnumerator<Employees> enumerator = developers.GetEnumerator(); // low level code
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current.Name);
            }

            foreach ( var em in developers.Where(e => e.Name.StartsWith("C")))
            {
                Console.WriteLine("Developer Name start with C is {0}", em.Name);
            }
        }

        //private static bool NameStartWithC(Employees employee)
        //{
        //    return employee.Name.StartsWith("C");
        //}
    }
}
