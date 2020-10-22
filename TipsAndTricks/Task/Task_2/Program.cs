using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Async_2
{
    class Program
    {
        static async Task Main()
        {
            var sw = Stopwatch.StartNew();
        
            await Task.WhenAll(Method(), Method(), Method());
        
            Console.WriteLine(sw.Elapsed);
        }
        
        static async Task Method()
        {
            await Task.Delay(TimeSpan.FromSeconds(5));
        }
        
        // static async Task Main()
        // {
        //     var sw = Stopwatch.StartNew();
        //
        //     var t1 = Task.Run(Method);
        //     var t2 = Task.Run(Method);
        //     //var t3 = Task.Run(Method);
        //     Method();
        //
        //     await Task.WhenAll(t1, t2);
        //
        //     Console.WriteLine(sw.Elapsed);
        // }
        //
        // static void Method()
        // {
        //     Task.Delay(TimeSpan.FromSeconds(5)).Wait();
        // }
    }
}