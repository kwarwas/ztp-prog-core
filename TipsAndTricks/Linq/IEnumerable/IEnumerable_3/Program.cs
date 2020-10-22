using System;
using System.Collections.Generic;
using System.Linq;

namespace IEnumerable_3
{
    class Program
    {
        static void Main()
        {
            ICollection<int> collection = Method(new[] { 5, 6, 7 }).ToArray();

            Console.WriteLine(string.Join(", ", collection));
            Console.WriteLine(string.Join(", ", collection));
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