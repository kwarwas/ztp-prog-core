using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Context;
using Messages;
using Microsoft.Extensions.Logging;

namespace Sender
{
    class Program
    {
        static async Task Main()
        {
            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter(typeof(Program).FullName, LogLevel.Debug)
                    .AddConsole();
            });
            
            LogContext.ConfigureCurrentLogContext(loggerFactory);

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