using System;
using System.Threading.Tasks;
using MassTransit;
using Messages;
using Serilog;
using Serilog.Events;

namespace Sender
{
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

            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost/"), hostCfg =>
                {
                    hostCfg.Username("guest");
                    hostCfg.Password("guest");
                });

                cfg.UseSerilog();
            });

            await bus.StartAsync();

            Console.WriteLine("Bus started");

            var client = bus.CreateRequestClient<IOrderRequest>(new Uri("rabbitmq://localhost/order-req-res"));

            var guid = Guid.NewGuid();
            var now = DateTime.Now;

            var response = await client.GetResponse<IOrderResponse>(new
            {
                Name = $"Order {guid}",
                Weight = 134.5
            });

            Console.WriteLine($"Order {guid} sent at {now} - response: {response.Message.Name}, {response.Message.HandledDate}");

            Console.ReadLine();

            Console.WriteLine("Bus stopping...");

            await bus.StopAsync();

            Console.WriteLine("Bus stopped");
        }
    }
}