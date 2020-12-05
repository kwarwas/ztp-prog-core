using System;
using System.Reflection;
using System.Threading.Tasks;
using CQRS.Core.Command;
using CQRS.Core.Event;
using CQRS.Core.Infrastructure.Cqrs.Commands;
using CQRS.Core.Infrastructure.Cqrs.Events;
using CQRS.Model.WriteModel;
using CQRS.WriteSide.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CQRS.WriteSide
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // MIGRATION
            // dotnet ef migrations add Init
            
            // RUN
            // dotnet ef database update

            var serviceProvider = new ServiceCollection()
                .AddMediatR(Assembly.GetExecutingAssembly())
                .AddSingleton<ICommandBus, CommandBus>()
                .AddSingleton<IEventsBus, EventBus>()
                .BuildServiceProvider();

            var commandBus = serviceProvider.GetService<ICommandBus>();

            if (commandBus is null)
                return;

            Console.WriteLine("Saving person...");
            await commandBus.Send(new SavePerson("Jan", "Kowalski"));
            Console.WriteLine("Saving addresses...");
            await commandBus.Send(new SaveAddress(1, "Bielsko-Biała", "43-300", "Willowa", "2", AddressType.Main));
            await commandBus.Send(new SaveAddress(1, "Bielsko-Biała", "43-300", "Bystrzańska", "100"));
            await commandBus.Send(new SaveAddress(1, "Bielsko-Biała", "43-300", "Rynek", "10"));

            Console.WriteLine("Saving person...");
            await commandBus.Send(new SavePerson("Adam", "Nowak"));
            await commandBus.Send(new SaveAddress(2, "Katowice", "43-300", "Mikołowska", "222c", AddressType.Main));

            Console.WriteLine("Data saved");
        }
    }
}