using System;
using System.Threading.Tasks;
using MassTransit;
using Messages;

namespace Receiver_2
{
    class OrderMessageConsumer : IConsumer<IOrderMessage>
    {
        private readonly Guid _consumerId = Guid.NewGuid();

        public async Task Consume(ConsumeContext<IOrderMessage> context)
        {
            await Console.Out.WriteLineAsync($"Handler {_consumerId}: {context.Message.Name} {DateTime.Now}");
        }
    }

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

                cfg.ReceiveEndpoint("order-service-2", e => { e.Consumer<OrderMessageConsumer>(); });
            });

            await bus.StartAsync();

            Console.WriteLine("Bus started (receiver 2)");

            Console.ReadLine();

            bus.Stop();

            Console.WriteLine("Bus stopped");
        }
    }
}