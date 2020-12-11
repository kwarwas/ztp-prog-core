using Akka.Actor;
using ActorTypes.Messages;
using System;

namespace ActorTypes.Actors
{
    public class OrderReceiveActor : ReceiveActor
    {
        public OrderReceiveActor()
        {
            Receive<OrderMessage>(message => Console.WriteLine("Receive message: {0} {1}", message.Id, message.Name));
        }
    }
}