using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IAsyncEnumerable_2
{
    class Program
    {
        static async Task Main()
        {
            Console.WriteLine(DateTime.Now);

            var list = GetData()
                .Skip(3)
                .Take(3);

            Console.WriteLine(DateTime.Now);
            
            await foreach (var item in list)
            {
                Console.WriteLine("{0} {1}", DateTime.Now, item);
            }
        }
        
        static async IAsyncEnumerable<(DateTime, int)> GetData()
        {
            var index = 0;
            while (true)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                yield return (DateTime.Now, index++);
            }
        }
    }
}