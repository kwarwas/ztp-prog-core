using System;
using System.Threading.Tasks;
using MassTransit;
using Messages;
using Serilog;
using Serilog.Events;

namespace Receiver
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

                cfg.ReceiveEndpoint(host, "order-req-res", e =>
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