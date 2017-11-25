using ActorBecome.Actors;
using ActorBecome.Messages;
using Akka.Actor;
using System;

namespace ActorBecome
{
    class Program
    {
        static void Main(string[] args)
        {
            var system = ActorSystem.Create("ActorBecome");

            var actor = system.ActorOf<MarketReceiveActor>();

            actor.Tell(new OrderMessage(1, "Akka.NET Book 1"));

            Console.ReadLine();

            actor.Tell(new MarketOpenMessage());
            actor.Tell(new OrderMessage(2, "Akka.NET Book 2"));
            actor.Tell(new OrderMessage(3, "Akka.NET Book 3"));
            actor.Tell(new MarketOpenMessage());

            Console.ReadLine();

            actor.Tell(new MarketCloseMessage());
            actor.Tell(new OrderMessage(4, "Akka.NET Book 4"));
            actor.Tell(new MarketCloseMessage());

            Console.ReadLine();

            actor.Tell(new MarketOpenMessage());
            actor.Tell(new OrderMessage(5, "Akka.NET Book 5"));
            actor.Tell(new OrderMessage(6, "Akka.NET Book 6"));
            actor.Tell(new OrderMessage(7, "Akka.NET Book 7"));

            Console.ReadLine();
        }
    }
}
