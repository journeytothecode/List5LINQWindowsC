using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace List5LINQWindowsC
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\windows";
            Console.WriteLine("*** List All the Files ***");
            ShowLargeFileinLinq(path);
            Console.WriteLine();
            Console.WriteLine("*** With SQL syntax ***");
            LinqSqlSyntax(path);
            Console.WriteLine();
            Console.WriteLine("*** With Lambda expression ***");
            LinqLambdaExpression(path);
        }

        private static void LinqLambdaExpression(string path)
        {
            // a serial of methods call
            var LambdaQ = new DirectoryInfo(path).GetFiles().OrderByDescending(f => f.Length).Take(5);

            foreach (FileInfo file in LambdaQ)
            {
                Console.WriteLine($"{file.Name , -30} : {file.Extension , 10} : {file. Length, 30:N0}");
            }
        }

        private static void LinqSqlSyntax(string path)
        {
            var query = from file in new DirectoryInfo(path).GetFiles()
                        orderby file.Length descending // compare in decending mode
                        select file;
            foreach (FileInfo file in query.Take(5)) // invoke a Linq operator
            {
                Console.WriteLine($"{file.Name, -30} : {file.Extension, 10} : {file.Length, 30:N0}");
            }
        }

        private static void ShowLargeFileinLinq(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            FileInfo[] files = directory.GetFiles();

            //print out the array of files
            foreach (FileInfo file in files)
            {
                Console.WriteLine($"{file.Name,-30}: {file.Extension,15} : {file.Length,30:N0}"); // C#6 string interperlation 
            }

        }
    }
}
