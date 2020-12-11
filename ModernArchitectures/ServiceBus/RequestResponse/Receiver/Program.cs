using System;
using System.Threading.Tasks;
using MassTransit;
using Messages;

namespace Receiver
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

                cfg.ReceiveEndpoint("order-req-res", e =>
                {
                    e.Handler<IOrderRequest>(async x =>
                        {
                            await Console.Out.WriteLineAsync($"Handler: {x.Message.Name} {DateTime.Now}");

                            await x.RespondAsync<IOrderResponse>(new
                            {
                                x.Message.Name,
                                HandledDate = DateTime.Now
                            });
                        }
                    );
                });
            });

            await bus.StartAsync();

            Console.WriteLine("Bus started");

            Console.ReadLine();

            await bus.StopAsync();

            Console.WriteLine("Bus stopped");
        }
    }
}