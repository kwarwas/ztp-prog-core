using ActorPersistenceSnapshot.Actors;
using ActorPersistenceSnapshot.Commands;
using ActorPersistenceSnapshot.Events;
using Akka.Actor;
using Akka.Configuration;
using MongoDB.Bson.Serialization;

namespace ActorPersistenceSnapshot
{
    class Program
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
                    snapshot-store {
                        plugin = ""akka.persistence.snapshot-store.mongodb""
                        mongodb {
                            class = ""Akka.Persistence.MongoDb.Snapshot.MongoDbSnapshotStore, Akka.Persistence.MongoDb""
                            connection-string = ""mongodb://localhost/akka""
                            collection = ""SnapshotStore""
                        }
                    }
                }
            }");
            
            BsonClassMap.RegisterClassMap<OrderDetailsAdded>();

            var system = ActorSystem.Create("ActorPersistence", config);

            var actor = system.ActorOf(Props.Create<OrderActor>("ORD003"));

            actor.Tell(new AddOrderDetails("Akka.NET Book 1", 10.5m));
            actor.Tell(new AddOrderDetails("Akka.NET Book 2", 12.5m));
            actor.Tell(new AddOrderDetails("Akka.NET Book 3", 34.5m));
            actor.Tell(new AddOrderDetails("Akka.NET Book 4", 34.5m));
            actor.Tell(new AddOrderDetails("Akka.NET Book 5", 34.5m));
            actor.Tell(new AddOrderDetails("Akka.NET Book 6", 34.5m));

            actor.Tell(new CalculatePrice());

            actor.Tell(new AddOrderDetails("Akka.NET Book 7", 10.5m));

            actor.Tell(new ThrowError());

            actor.Tell(new AddOrderDetails("Akka.NET Book 8", 10.5m));
            actor.Tell(new AddOrderDetails("Akka.NET Book 9", 10.5m));
            actor.Tell(new AddOrderDetails("Akka.NET Book 10", 10.5m));
            actor.Tell(new AddOrderDetails("Akka.NET Book 11", 10.5m));
            actor.Tell(new AddOrderDetails("Akka.NET Book 12", 10.5m));
            actor.Tell(new AddOrderDetails("Akka.NET Book 13", 10.5m));

            actor.Tell(new CalculatePrice());

            system.WhenTerminated.Wait();
        }
    }
}
