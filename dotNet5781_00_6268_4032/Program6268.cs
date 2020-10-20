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
            welcome6268();
            welcome4032();
            Console.ReadKey();
        }
        static partial void welcome4032();

        private static void welcome6268()
        {
            Console.WriteLine("enter your name: ");
            string s = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", s);
        }
    }
}
