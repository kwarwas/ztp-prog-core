using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IAsyncEnumerable_1
{
    class Program
    {
        static async Task Main()
        {
            await foreach (var item in GetData())
            {
                Console.WriteLine("{0} {1}", DateTime.Now, item);
            }
        }
        
        static async IAsyncEnumerable<int> GetData()
        {
            for (var i = 1; i <= 10; i++)
            {
                await Task.Delay(TimeSpan.FromSeconds(1)); 
                yield return i;
            }
        }
    }
}