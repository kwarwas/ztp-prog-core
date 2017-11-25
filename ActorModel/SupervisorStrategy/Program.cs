using System;
using System.Threading.Tasks;
using ActorSupervisorStrategy.Actors;
using ActorSupervisorStrategy.Messages;
using Akka.Actor;

namespace ActorSupervisorStrategy
{
    class Program
    {
        static void Main(string[] args)
        {
            var system = ActorSystem.Create("ActorSupervisorStrategy");

            var orderReceiveActor = system.ActorOf<OrderReceiveActor>();

            orderReceiveActor.Tell(new OrderMessage(10, "Akka"));

            Task.Delay(TimeSpan.FromSeconds(1)).Wait();

            orderReceiveActor.Tell(new OrderMessage(10, "Akka.NET"));

            system.WhenTerminated.Wait();
        }
    }
}
