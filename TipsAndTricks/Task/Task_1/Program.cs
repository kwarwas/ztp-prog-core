using System;
using System.Threading.Tasks;

namespace Async_1
{
    class Program
    {
        // version 1
        static void Main()
        {
            M1().Wait();
            M2();
            //Console.WriteLine(M3().Result);
            Console.WriteLine(M4().Result);
            Console.WriteLine(M5().Result);
            Console.WriteLine(M6().Result);
        
            Console.WriteLine("Finish");
            Console.ReadLine();
        }
        
        // version 2
        // static async Task Main()
        // {
        //     await M1();
        //     M2();
        //     //Console.WriteLine(await M3());
        //     Console.WriteLine(await M4());
        //     Console.WriteLine(await M5());
        //     Console.WriteLine(await M6());
        //
        //     Console.WriteLine("Finish");
        //     Console.ReadLine();
        // }

        static async Task M1()
        {
            Console.WriteLine("M1 starting...");
            await Task.Delay(TimeSpan.FromSeconds(2));
            Console.WriteLine("M1 has finished.");
        }

        static async void M2()
        {
            Console.WriteLine("M2 starting...");
            await Task.Delay(TimeSpan.FromSeconds(2));
            Console.WriteLine("M2 has finished.");
        }

        //see warning
        //static async Task<int> M3()
        //{
        //    return 2;
        //}

        static async Task<int> M4()
        {
            return await Task.Run(() => 4);
        }

        static async Task<int> M5()
        {
            return await Task.FromResult(5);
        }
        
        static Task<int> M6()
        {
            return Task.FromResult(6);
        }
    }
}