using System;
using System.Threading.Tasks;
using Akka.Actor;
using CQRS.Core.Command;
using CQRS.WriteSide.Commands;
using CQRS.WriteSide.Database.WriteModel;

namespace CQRS.WriteSide
{
    class Program
    {
        static void Main()
        {
            //// RUN
            // docker run -e MYSQL_ROOT_PASSWORD=password -p 3306:3306 -d dnhsoft/mysql-utf8
            // dotnet ef database update
            
            using (var actorSystem = ActorSystem.Create("CQRS"))
            {
                SaveData(actorSystem).Wait();
            }
        }

        private static async Task SaveData(IActorRefFactory actorRefFactory)
        {
            var commandRootActor = actorRefFactory.ActorOf<CommandRootActor>();
            var eventRootActor = actorRefFactory.ActorOf<EventRootActor>("EventRootActor");
            
            Console.WriteLine("Saving person...");
            var idCommandResult = await commandRootActor.Ask<IdCommandResult>(new SavePerson("Jan", "Kowalski"));
            Console.WriteLine("Saving addresses...");
            await commandRootActor.Ask<CommandResult>(new SaveAddress(idCommandResult.Id, "Bielsko-Biała", "43-300", "Willowa", "2", AddressType.Main));
            await commandRootActor.Ask<CommandResult>(new SaveAddress(idCommandResult.Id, "Bielsko-Biała", "43-300", "Bystrzańska", "100"));
            await commandRootActor.Ask<CommandResult>(new SaveAddress(idCommandResult.Id, "Bielsko-Biała", "43-300", "Rynek", "10"));

            Console.WriteLine("Saving person...");
            idCommandResult = await commandRootActor.Ask<IdCommandResult>(new SavePerson("Adam", "Nowak"));
            await commandRootActor.Ask<CommandResult>(new SaveAddress(idCommandResult.Id, "Katowice", "43-300", "Mikołowska", "222c", AddressType.Main));
            Console.WriteLine("Saving addresses...");

            Console.WriteLine("Data saved");

            Console.ReadLine();
        }
    }
}