using ActorStopping.Actors;
using ActorStopping.Messages;
using Akka.Actor;
using Akka.Event;
using System;

namespace ActorStopping
{
    class Program
    {
        static void Main()
        {
            var system = ActorSystem.Create("ActorTypes");

            var actor = system.ActorOf<OrderReceiveActor>();

            var subscriber = system.ActorOf<DeadLetterReceiverActor>();
            system.EventStream.Subscribe(subscriber, typeof(DeadLetter));

            actor.Tell(new OrderMessage(1, "Akka.NET Book 1"));
            actor.Tell(new OrderMessage(2, "Akka.NET Book 2"));
            actor.Tell(new OrderMessage(3, "Akka.NET Book 3"));
            actor.Tell(new OrderMessage(4, "Akka.NET Book 4"));
            actor.Tell(new OrderMessage(5, "Akka.NET Book 5"));
            actor.Tell(PoisonPill.Instance);
            actor.Tell(new OrderMessage(6, "Akka.NET Book 6"));
            actor.Tell(new OrderMessage(7, "Akka.NET Book 7"));

            Console.ReadLine();
        }
    }
}
