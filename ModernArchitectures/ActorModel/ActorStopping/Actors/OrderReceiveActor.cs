using Akka.Actor;
using ActorStopping.Messages;
using System;

namespace ActorStopping.Actors
{
    public class OrderReceiveActor : ReceiveActor
    {
        public OrderReceiveActor()
        {
            Receive<OrderMessage>(message => Console.WriteLine("Receive message: {0} {1}", message.Id, message.Name));
        }
    }
}