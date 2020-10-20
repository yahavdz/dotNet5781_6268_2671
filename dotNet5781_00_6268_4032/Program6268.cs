using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_00_6268_4032
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome6268();
            Welcome4032();
            Console.ReadKey();
        }
        static partial void Welcome4032();

        private static void Welcome6268()
        {
            Console.WriteLine("enter your name: ");
            string s = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", s);
        }
    }
}
