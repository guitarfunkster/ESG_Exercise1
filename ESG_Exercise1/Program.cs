using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESG_Exercise1
{
    internal static class Program
    {
        public static void Main(String[] args)
        {
            // See https://aka.ms/new-console-template for more information
            Console.WriteLine("Calculator program.See tests project");

            var input = "1,2,3";
            var res = Calculator.Add(input);
            Console.WriteLine($"The calculator's sum of [{input}] returns {res}");

        }

    }
}
