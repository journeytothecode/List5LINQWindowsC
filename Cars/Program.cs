using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars
{
    class Program
    {
        static void Main(string[] args)
        {
            var cars = ProcessFile("fuel.csv");
            var query = cars.OrderByDescending(c => c.Combined)
                .ThenBy(c => c.Name)
                .Select(c => new { c.Name, c.Manufacturer, c.Year, c.Combined });// projecting a new data type with Lambda

            //Console.WriteLine($"{query.Name}:{query.Manufacturer}");

            foreach (var item in query.Take(10))
            {
                Console.WriteLine($"{item.Name,-25} : {item.Combined,10} : {item.Year,5} : {item.Manufacturer,30}");
            }

            var manufactures = ProcessManufacture("manufacturers.csv");

            var manuQuery = manufactures
                .OrderBy(m => m.Name)
                .Select(m => m);

            foreach (var item in manuQuery)
            {
                Console.WriteLine($"{item.Name,-10} : {item.Headquarter,15} : {item.Year,4}");
            }
        }//

        private static List<Manufacture> ProcessManufacture(string path)
        {
            return
                File.ReadAllLines(path)
                .Skip(0)
                .Where(m => m.Length > 1)
                .Select(m =>
                {
                    var columns = m.Split(',');
                    return new Manufacture
                    {
                        Name = columns[0],
                        Headquarter = columns[1],
                        Year = int.Parse(columns[2])
                    };
                }).ToList();
        }

        private static List<Car> ProcessFile(string path)
        {
            return
            File.ReadAllLines(path)
                .Skip(1) //pagination
                .Where(L => L.Length > 1)
                .Select(Car.ParseFromCSV)
                .ToList();
        }
    }
}
