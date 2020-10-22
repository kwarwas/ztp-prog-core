using System;
using System.IO;
using System.Linq;
using Akka.Actor;
using Akka.Configuration;
using Akka.Routing;
using NonSeedNode.Actors;
using NonSeedNode.Messages;

namespace NonSeedNode
{
    public static class HoconLoader
    {
        public static Config FromFile(string path)
        {
            return ConfigurationFactory.ParseString(File.ReadAllText(path));
        }
    }

    class Program
    {
        static void Main()
        {
            using (var system = ActorSystem.Create("ActorCluster", HoconLoader.FromFile("config.hocon")))
            {
                Console.ReadLine();
                
                var props = Props.Create<OrderActor>().WithRouter(FromConfig.Instance);

                var actor = system.ActorOf(props, "OrderActor");

                Console.ReadLine();

                Enumerable.Range(1, 10).ToList().ForEach
                (
                    x => actor.Tell(new OrderMessage(x, "Akka.NET Book"))
                );

                Console.ReadLine();
            }
        }
    }
}