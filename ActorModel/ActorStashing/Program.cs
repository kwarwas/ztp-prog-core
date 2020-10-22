using ActorStashing.Actors;
using ActorStashing.Messages;
using Akka.Actor;
using System;

namespace ActorStashing
{
    class Program
    {
        static void Main()
        {
            var system = ActorSystem.Create("ActorStashing");

            var actor = system.ActorOf<OrderActor>();

            for (int i = 1; i <= 5; i++)
            {
                actor.Tell(new OrderMessage(i, $"Akka.NET Book {i}"));
            }

            Console.WriteLine("All messages have been sent");

            system.WhenTerminated.Wait();
        }
    }
}

