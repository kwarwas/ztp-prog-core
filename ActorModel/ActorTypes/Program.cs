using ActorTypes.Actors;
using ActorTypes.Messages;
using Akka.Actor;
using System;

namespace ActorTypes
{
    class Program
    {
        static void Main(string[] args)
        {
            var system = ActorSystem.Create("ActorTypes");

            var untypedActor = system.ActorOf<OrderUntypedActor>();
            var typedActor = system.ActorOf<OrderTypedActor>();
            var receiveActor = system.ActorOf<OrderReceiveActor>();

            untypedActor.Tell(new OrderMessage(10, "Akka.NET Book"));
            typedActor.Tell(new OrderMessage(10, "Akka.NET Book"));
            receiveActor.Tell(new OrderMessage(10, "Akka.NET Book"));

            Console.ReadLine();
        }
    }
}
