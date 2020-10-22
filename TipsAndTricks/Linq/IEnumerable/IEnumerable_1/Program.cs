using System;
using System.Linq;

namespace IEnumerable_1
{
    class Program
    {
        static void Main()
        {
            var list = Enumerable
                .Range(1, 2)
                .Select(x => Guid.NewGuid());

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }
    }
}