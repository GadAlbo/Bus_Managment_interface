using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_00_9047_4960
{
    partial class Program
    {
        static void Main(string[] args)
        {
            welcome4960();
            Console.ReadKey();
        }

        private static void welcome4960()
        {
            Console.WriteLine("Enter your name: ");
            string myString = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", myString);
        }
        static partial void welcome9047();
    }
}