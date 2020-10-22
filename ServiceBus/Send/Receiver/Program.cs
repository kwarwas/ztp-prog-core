using System;
using System.Threading.Tasks;
using MassTransit;
using Messages;
using Serilog;
using Serilog.Events;

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

                cfg.ReceiveEndpoint(host, "submit-order", e =>
                {
                    e.Consumer<SubmitOrderConsumer>();
                });
                
                cfg.UseSerilog();
            });

            await bus.StartAsync();

            Console.WriteLine("Bus started");

            Console.ReadLine();

            await bus.StopAsync();

            Console.WriteLine("Bus stopped");
        }
    }
}