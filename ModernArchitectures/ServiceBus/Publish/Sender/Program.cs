using System;
using System.Threading.Tasks;
using MassTransit;
using Messages;

namespace Sender
{
    class Program
    {
        static async Task Main()
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri("rabbitmq://localhost/"), hostCfg =>
                {
                    hostCfg.Username("guest");
                    hostCfg.Password("guest");
                });
            });

            await bus.StartAsync();

            Console.WriteLine("Bus started");

            for (int i = 0; i < 2; i++)
            {
                var guid = Guid.NewGuid();

                var message = new
                {
                    Name = $"Order {guid}"
                };
                
                await bus.Publish<IOrderMessage>(message);
             
                Console.WriteLine($"{message} sent");
            }

            await bus.StopAsync();

            Console.WriteLine("Bus stopped");
        }
    }
}