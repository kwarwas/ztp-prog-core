using Akka.Actor;
using System;
using ActorDialogue.Actors;
using ActorDialogue.Messages;

namespace ActorDialogue
{
    class Program
    {
        static void Main(string[] args)
        {
            var system = ActorSystem.Create("ActorDialogue");

            var untypedActor = system.ActorOf<OrderForwardActor>();

            untypedActor.Tell(new OrderMessage(10, new[] { "Prod", "Pro" }));
            
            Console.ReadLine();
        }
    }
}
