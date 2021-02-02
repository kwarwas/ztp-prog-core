using System;
using System.Threading.Tasks;

namespace Task_3
{
    class Program
    {
        static async Task Main()
        {
            var a = await await Task.Run(() => 1).ContinueWith(x => x);
            var b = await Task.Run(() => 1).ContinueWith(x => x).Unwrap();
            
            Console.WriteLine(a);
            Console.WriteLine(b);
        }
    }
}