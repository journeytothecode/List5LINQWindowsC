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
            //join Car's Manufacture with Manufacture's name
            var joinQ = cars.Join(manufactures,
                c => new { c.Manufacturer, c.Year},
                m => new { Manufacturer = m.Name, m.Year},
                (c, m) => new
                {
                    m.Headquarter,
                    c.Name,
                    c.Manufacturer,
                    c.Combined,
                    c.Year,

                })
                .OrderByDescending(c => c.Combined)
                .ThenBy(c => c.Name.Length);

            foreach (var i in joinQ.Take(10))
            {
                Console.WriteLine($"after Join : {i.Headquarter,-15} : {i.Name,15} : {i.Manufacturer,30} : {i.Year,4}: fuel effecsioncy Combined {i.Combined}");
            }

            //var joinQ2 = cars.Join(manufactures,c =>c.Manufacturer,m =>m.Name,(c,m));
            var joiQ2 = cars.Join(manufactures, c => c.Manufacturer, m => m.Name, (c, m) => new
            {
                Car = c,
                Manufacture = m
            }). OrderByDescending(c => c.Car.Combined);

            foreach (var i in joiQ2.Take(10))
            {
                Console.WriteLine($"{i.Car.Name,-20}: {i.Car.Combined,20} : {i.Manufacture.Name,20} : {i.Manufacture.Headquarter,20}");
            }

            //group by

            var qGroup = cars.GroupBy(c => c.Manufacturer.ToUpper())
                .OrderByDescending(g => g.Key)
                .ThenBy(c => c.OrderByDescending(e => e.Name));
            // group by Manufacturer which combine is deceding order

            foreach (var group in qGroup)
            {
                Console.WriteLine($" Group by Manufacturer {group.Key} : ");
                foreach (var car in group.OrderByDescending( c=> c.Combined).Take(3))
                {
                    Console.WriteLine($"{car.Name,-15} : {car.Combined,20}");
                }
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
