using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Messages;
using Serilog;
using Serilog.Events;

namespace Sender
{
    class Program
    {
        static async Task Main()
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