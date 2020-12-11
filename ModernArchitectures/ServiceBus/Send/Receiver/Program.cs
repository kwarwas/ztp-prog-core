using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Context;
using Messages;
using Microsoft.Extensions.Logging;

namespace Receiver
{
    class SubmitOrderConsumer: IConsumer<ISubmitOrder>
    {
        public async Task Consume(ConsumeContext<ISubmitOrder> context)
        {
            await Console.Out.WriteLineAsync($"Receive message id {context.Message.Id}");
        }
    }
    
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

                cfg.ReceiveEndpoint("submit-order", e =>
                {
                    e.Consumer<SubmitOrderConsumer>();
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