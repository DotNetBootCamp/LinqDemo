﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var linqDemo = new LinqDemo();
            linqDemo.Deferred_Execution_Sample();

            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
