using System;
using Messages;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using RawRabbit.Configuration;
using RawRabbit.DependencyInjection.ServiceCollection;
using RawRabbit.Instantiation;

namespace Sender
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddRawRabbit(new RawRabbitOptions
                {
                    ClientConfiguration = new RawRabbitConfiguration
                    {
                        Username = "guest",
                        Password = "guest",
                        Port = 5672,
                        VirtualHost = "/",
                        Hostnames = {"localhost"}
                    }
                })
                .BuildServiceProvider();

            var client = serviceProvider.GetService<IBusClient>();

            Console.WriteLine("Publishing order event...");

            var orderSubmitted = new OrderSubmitted(Guid.NewGuid(), $"Order - {DateTime.Now}", 1230);
            
            client.PublishAsync(orderSubmitted).Wait();
            
            Console.WriteLine($"Event {orderSubmitted.Id} published.");            
        }
    }
}