using System;
using System.Threading.Tasks;
using Messages;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using RawRabbit.DependencyInjection.ServiceCollection;

namespace Receiver
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddRawRabbit()
                .BuildServiceProvider();

            var client = serviceProvider.GetService<IBusClient>();

            Console.WriteLine("Waiting for command...");

            client.RespondAsync<SubmitOrder, SubmitOrderResponse>(command =>
            {
                Console.WriteLine("Received: {0}", command.Name);
                return Task.FromResult(new SubmitOrderResponse("Ok"));
            });
        }
    }
}