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

            var sendEndpoint = await bus.GetSendEndpoint(new Uri("rabbitmq://localhost/submit-order"));
            
            for (int i = 0; i < 2; i++)
            {
                var message = new
                {
                    Id = Guid.NewGuid()
                };
                
                await sendEndpoint.Send<ISubmitOrder>(message);
             
                Console.WriteLine($"{message} sent");
            }

            await bus.StopAsync();

            Console.WriteLine("Bus stopped");
        }
    }
}