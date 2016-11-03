using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferedExecultion
{
    class Program
    {
        static void Main(string[] args)
        {
            var movies = new List<Movie>
            {
                new Movie {Title="Spiderman", Rating= 8.5f, Year=2008 },
                new Movie {Title="Dogman", Rating=9.6f, Year=2009 },
                new Movie {Title="Hentaiman", Rating =8.0f, Year= 2013 },
                new Movie {Title="Red underwear man", Rating = 7.0f, Year= 2015 }
            };

            var q = movies.Where(m => m.Rating >= 8.0).ToList();
            var qy = movies.Where(my => my.Year >2009);

            //foreach (var m in q)
            //{
            //    Console.WriteLine("Movie has high rate {0} with {1}",m.Title, m.Rating);
            //}
            foreach (var m in qy)
            {
                Console.WriteLine("Movie has high rate {0} with {1}", m.Title, m.Rating);
            }
        }
    }
}
