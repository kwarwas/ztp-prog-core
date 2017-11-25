using ActorRouters.Actors;
using ActorRouters.Messages;
using Akka.Actor;
using Akka.Routing;
using System;
using System.Linq;

namespace ActorRouters
{
    class Program
    {
        static void Main(string[] args)
        {
            var system = ActorSystem.Create("ActorRouters");

            var props = Props.Create<OrderActor>().WithRouter(new RoundRobinPool(5));

            var actor = system.ActorOf(props);

            Enumerable.Range(1, 10).ToList().ForEach
            (
                x => actor.Tell(new OrderMessage(x, "Akka.NET Book"))
            );

            Console.ReadLine();
        }
    }
}
