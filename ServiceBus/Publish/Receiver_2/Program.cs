using System;
using System.Threading.Tasks;
using MassTransit;
using Messages;
using Serilog;
using Serilog.Events;

namespace Receiver_2
{
    class OrderMessageConsumer : IConsumer<IOrderMessage>
    {
        private readonly Guid _consumerId = Guid.NewGuid();

        public Task Consume(ConsumeContext<IOrderMessage> context)
        {
            Log.Information($"Handler {_consumerId}: {context.Message.Name} {DateTime.Now}");
            return Task.CompletedTask;
        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            var consumer = new OrderMessageConsumer();

            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost/"), hostCfg =>
                {
                    hostCfg.Username("guest");
                    hostCfg.Password("guest");
                });

                cfg.UseSerilog();

                cfg.ReceiveEndpoint(host, "order-service-2", e => { e.Consumer<OrderMessageConsumer>(); });
            });

            await bus.StartAsync();

            Console.WriteLine("Bus started (receiver 2)");

            Console.ReadLine();

            bus.Stop();

            Console.WriteLine("Bus stopped");
        }
    }
}