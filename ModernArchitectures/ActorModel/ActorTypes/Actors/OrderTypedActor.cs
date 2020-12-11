using Akka.Actor;
using ActorTypes.Messages;
using System;

namespace ActorTypes.Actors
{
    public class OrderTypedActor : TypedActor, IHandle<OrderMessage>
    {
        public void Handle(OrderMessage message)
        {
            Console.WriteLine("Receive message: {0} {1}", message.Id, message.Name);
        }
    }
}