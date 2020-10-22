using System;
using System.Threading.Tasks;

namespace Task_3
{
    class Program
    {
        static async Task Main()
        {
            var a = await await Task.Run(() => 1).ContinueWith(x => x);
            
            Console.WriteLine(a);
        }
    }
}