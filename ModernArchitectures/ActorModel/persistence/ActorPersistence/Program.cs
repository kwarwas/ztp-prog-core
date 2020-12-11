using ActorPersistence.Actors;
using ActorPersistence.Messages;
using Akka.Actor;
using Akka.Configuration;
using MongoDB.Bson.Serialization;

namespace ActorPersistence
{
    public class Program
    {
        static void Main()
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
            
            BsonClassMap.RegisterClassMap<OrderDetailsMessage>();

            var system = ActorSystem.Create("ActorPersistence", config);

            var actor = system.ActorOf(Props.Create<OrderActor>("ORD001"));

            actor.Tell(new OrderDetailsMessage("Akka.NET Book 1", 10.5m));
            actor.Tell(new OrderDetailsMessage("Akka.NET Book 2", 12.5m));
            actor.Tell(new OrderDetailsMessage("Akka.NET Book 3", 34.5m));

            actor.Tell(new PriceMessage());

            actor.Tell(new OrderDetailsMessage("Akka.NET Book 4", 10.5m));

            actor.Tell(new ErrorMessage());

            actor.Tell(new OrderDetailsMessage("Akka.NET Book 5", 10.5m));
            actor.Tell(new OrderDetailsMessage("Akka.NET Book 6", 10.5m));

            actor.Tell(new PriceMessage());

            system.WhenTerminated.Wait();
        }
    }
}
