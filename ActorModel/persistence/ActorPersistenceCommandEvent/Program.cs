using ActorPersistenceCommandEvent.Actors;
using ActorPersistenceCommandEvent.Commands;
using ActorPersistenceCommandEvent.Events;
using Akka.Actor;
using Akka.Configuration;
using MongoDB.Bson.Serialization;

namespace ActorPersistenceCommandEvent
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ConfigurationFactory.ParseString(@"
            akka {
                persistence {
                    publish-plugin-commands = on
                    journal {
                        plugin = ""akka.persistence.journal.mongodb""
                        mongodb {
                            class = ""Akka.Persistence.MongoDb.Journal.MongoDbJournal, Akka.Persistence.MongoDb""
                            connection-string = ""mongodb://localhost/akka""
                            collection = ""EventJournal""
                        }
                    }
                }
            }");
            
            BsonClassMap.RegisterClassMap<OrderDetailsAdded>();

            var system = ActorSystem.Create("ActorPersistence", config);

            var actor = system.ActorOf(Props.Create<OrderActor>("ORD002"));

            actor.Tell(new AddOrderDetails("Akka.NET Book 1", 10.5m));
            actor.Tell(new AddOrderDetails("Akka.NET Book 2", 12.5m));
            actor.Tell(new AddOrderDetails("Akka.NET Book 3", 34.5m));

            actor.Tell(new CalculatePrice());

            actor.Tell(new AddOrderDetails("Akka.NET Book 4", 10.5m));

            actor.Tell(new ThrowError());

            actor.Tell(new AddOrderDetails("Akka.NET Book 5", 10.5m));
            actor.Tell(new AddOrderDetails("Akka.NET Book 6", 10.5m));

            actor.Tell(new CalculatePrice());

            system.WhenTerminated.Wait();
        }
    }
}
