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
            //name method vs Lambda exprestion
            Func<int, int> square = x => x*x ; // delegate type Func and Action
            Func<int, int, int> total = (x, y) => x + y; // Func return type
            Func<int, int, int, int> suboftotal =(x,y,z) => { var temp = x - y;
                return temp + z;
                };
            Console.WriteLine("this is square of {0} equal to {1}",3, square(3));
            Console.WriteLine("this is total of {0} and {1} equal to {2}", 3,1, total(3,1));
            Console.WriteLine("this is sub {0} + (z ={1}) ", suboftotal(2,1,3),3);

            Action<int> printout = x => Console.WriteLine("{0} in Action delegate type",x);// action return Void

            printout(square(3));// call printout
            printout(total(5,5));
            printout(total(6,6));


            Action<int, int> print2 = (x,y) => Console.WriteLine("This is {0} and this is {1}",x,y);

            print2(square(2),total(3,3));
            print2(suboftotal(1,1,3),suboftotal(1,1,2));

            IEnumerable<Employees> developers = new Employees[]
            { //collections
                new Employees {Id = 1, Name = "Tom" },
                new Employees {Id = 2, Name = "Cruise" }
            };

            //Action<> printArray = x => Console.WriteLine("This is a array {0}",x);
            //printArray(developers);

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

            foreach (var item in developers.OrderBy(e => e.Name))
            {
                Console.WriteLine("order by name of dev {0}",item.Name);
            }

        }

        //private static bool NameStartWithC(Employees employee)
        //{
        //    return employee.Name.StartsWith("C");
        //}
    }
}
