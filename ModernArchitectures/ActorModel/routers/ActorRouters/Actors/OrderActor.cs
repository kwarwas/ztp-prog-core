using Akka.Actor;
using ActorRouters.Messages;
using System;

namespace ActorRouters.Actors
{
    public class OrderActor : ReceiveActor
    {
        static int counter = 0;
        public int ActorId { get; } = ++counter;

        public OrderActor()
        {
            Receive<OrderMessage>(Handle);
        }
        
        public void Handle(OrderMessage message)
        {
            Console.WriteLine("Actor #{0} Receive message: {1} {2}", ActorId, message.Id, message.Name);
        }
    }
}