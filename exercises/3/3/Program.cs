using System;
using System.Collections.Generic;

namespace GraphCreation
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = Console.ReadLine();
            int n = int.Parse(s);
            for(int i = 0 ;i < n ; i ++)
            {
                s = Console.ReadLine();
            }
            
            Console.WriteLine(n);
        }
    }
}
