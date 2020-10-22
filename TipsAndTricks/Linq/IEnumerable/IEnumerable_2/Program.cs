using System;
using System.Collections.Generic;

namespace IEnumerable_2
{
    class Program
    {
        static void Main()
        {
            Method(new[] { 1, 2, 3 });
            Method(new[] { 5, 6, 7 });
        }

        static IEnumerable<int> Method(int[] numbers)
        {
            Console.WriteLine("Method");

            foreach (var item in numbers)
            {
                yield return item + 2;
            }
        }
    }
}