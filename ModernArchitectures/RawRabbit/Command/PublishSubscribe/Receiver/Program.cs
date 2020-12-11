using System;
using Messages;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using RawRabbit.DependencyInjection.ServiceCollection;

namespace Receiver
{
    class Program
    {
        static void Main()
        {
            var serviceProvider = new ServiceCollection()
                .AddRawRabbit()
                .BuildServiceProvider();

            var client = serviceProvider.GetService<IBusClient>();

            Console.WriteLine("Waiting for incoming order command...");

            client.SubscribeAsync<SubmitOrder>
            (
                async message => await Console.Out.WriteLineAsync($"Recieved order: {message}.")
            ).Wait();
        }
    }
}