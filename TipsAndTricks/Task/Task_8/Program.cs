using System;
using System.Threading.Tasks;

namespace Task_4
{
    class Program
    {
        static async Task Main()
        {
            var parent = Task.Factory.StartNew(async () =>
            {
                Console.WriteLine("Outer started");
                await Task.Factory.StartNew( () =>
                {
                    Console.WriteLine("Inner started");
                    Task.Delay(TimeSpan.FromSeconds(2)).Wait();
                    Console.WriteLine("Inner finish");
                }, TaskCreationOptions.AttachedToParent);
            });
            
            await parent;
            
            Console.WriteLine($"Status: {parent.IsCompletedSuccessfully}");

        }
    }
}