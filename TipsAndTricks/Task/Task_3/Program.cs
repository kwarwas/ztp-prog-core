using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Async_3
{
    class Program
    {
        private static async Task Main()
        {
            Approach_1();
            await Approach_2();
        }

        private static void Approach_1()
        {
            static void Process(int index)
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                Console.WriteLine("{0} processed", index);
            }

            var sw = Stopwatch.StartNew();

            for (var i = 1; i <= 10; i++)
            {
                Process(i);
            }

            Console.WriteLine(sw.Elapsed);
        }

        private static async Task Approach_2()
        {
            static async Task Process(int index)
            {
                await Task
                    .Delay(TimeSpan.FromSeconds(1))
                    .ContinueWith(x => Console.WriteLine("{0} processed", index));
            }

            var sw = Stopwatch.StartNew();

            var tasks = Enumerable
                .Range(1, 10)
                .Select(Process);

            await Task.WhenAll(tasks.ToArray());

            Console.WriteLine(sw.Elapsed);
        }
    }
}