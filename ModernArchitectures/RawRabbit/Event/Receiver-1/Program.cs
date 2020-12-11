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

            Console.WriteLine("Waiting for incoming order submitted events...");

            client.SubscribeAsync<OrderSubmitted>
            (
                async message => await Console.Out.WriteLineAsync($"Recieved order: {message}."),
                ctx => ctx.UseSubscribeConfiguration
                (
                    cfg => cfg.FromDeclaredQueue(queue => queue.WithName("service-1"))
                )
            ).Wait();
        }
    }
}