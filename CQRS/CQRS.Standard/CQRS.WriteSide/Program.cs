using System;
using System.Threading.Tasks;
using CQRS.Core.Command;
using CQRS.Core.Common;
using CQRS.Core.Event;
using CQRS.WriteSide.Commands;
using CQRS.WriteSide.Database.WriteModel;
using CQRS.WriteSide.Events;

namespace CQRS.WriteSide
{
    class Program
    {
        static void Main(string[] args)
        {
            // NA PODSTAWIE
            //https://devstyle.pl/2016/11/10/cqrsdi-implementacja-w-c-i-autofac/
            
            // MIGRATION
            // dotnet ef migrations add Init
            
            // RUN
            // docker run -e MYSQL_ROOT_PASSWORD=password -p 3306:3306 -d dnhsoft/mysql-utf8
            // dotnet ef database update

            var eventHandlerRegistrator = new HandlerRegistrator<IEventHandler>();
            eventHandlerRegistrator.Register<PersonSaved>(new PersonListEventsHandler());
            eventHandlerRegistrator.Register<AddressSaved>(new PersonListEventsHandler());
            eventHandlerRegistrator.Register<PersonSaved>(new PersonDetailsEventHandler());
            var eventBus = new EventsBus(eventHandlerRegistrator);

            var commandHandlerRegistrator = new HandlerRegistrator<ICommandHandler>();
            commandHandlerRegistrator.Register<SavePerson>(new SavePersonCommandHandler(eventBus));
            commandHandlerRegistrator.Register<SaveAddress>(new SaveAddressCommandHandler(eventBus));
            var commandBus = new CommandBus(commandHandlerRegistrator);

            SaveData(commandBus).Wait();
        }

        private static async Task SaveData(ICommandBus commandBus)
        {
            Console.WriteLine("Saving person...");
            await commandBus.Send(new SavePerson("Jan", "Kowalski"));
            Console.WriteLine("Saving addresses...");
            await commandBus.Send(new SaveAddress(1, "Bielsko-Biała", "43-300", "Willowa", "2", AddressType.Main));
            await commandBus.Send(new SaveAddress(1, "Bielsko-Biała", "43-300", "Bystrzańska", "100"));
            await commandBus.Send(new SaveAddress(1, "Bielsko-Biała", "43-300", "Rynek", "10"));

            Console.WriteLine("Saving person...");
            await commandBus.Send(new SavePerson("Adam", "Nowak"));
            await commandBus.Send(new SaveAddress(2, "Katowice", "43-300", "Mikołowska", "222c", AddressType.Main));
            Console.WriteLine("Saving addresses...");

            Console.WriteLine("Data saved");
        }
    }
}