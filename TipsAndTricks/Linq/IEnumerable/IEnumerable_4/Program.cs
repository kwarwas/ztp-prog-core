using System;
using System.Collections.Generic;
using System.Linq;

namespace IEnumerable_4
{
    class Program
    {
        static void Main()
        {
            var numbers = Enumerable
                .Range(1, 49)
                .OrderBy(x => Guid.NewGuid())
                .Take(6)
                .OrderBy(x => x);

            Enumerable
                .Range(1, 10)
                .ToList()
                .ForEach(x => FormatString(x, numbers));
        }

        private static void FormatString(int x, IEnumerable<int> numbers)
        {
            Console.WriteLine($"# {x}");
            Console.WriteLine(string.Join(", ", numbers));
        }
    }
}