using System;
using System.Threading.Tasks;
using Messages;
using RawRabbit;
using RawRabbit.Configuration;
using RawRabbit.Instantiation;
using RawRabbit.Operations.Request.Middleware;

namespace Sender
{
    class Program
    {
        static void Main(string[] args)
        {
            var busConfig = new RawRabbitConfiguration
            {
                Username = "guest",
                Password = "guest",
                Port = 5672,
                VirtualHost = "/",
                Hostnames = {"localhost"},
                RequestTimeout = TimeSpan.FromMinutes(10)
            };

            SendCommand(busConfig).Wait();
        }

        private static async Task SendCommand(RawRabbitConfiguration busConfig)
        {
            var client = RawRabbitFactory.CreateSingleton(new RawRabbitOptions
            {
                ClientConfiguration = busConfig
            });

            Console.WriteLine("Sending command and waiting for response...");

            var submitOrder = new SubmitOrder(Guid.NewGuid(), $"Order - {DateTime.Now}", 1230);

            var response = await client.RequestAsync<SubmitOrder, SubmitOrderResponse>(submitOrder);

            Console.WriteLine($"Command {submitOrder.Id} sent successfully.");            

            Console.WriteLine($"Response: {response.Message}");
        }
    }
}