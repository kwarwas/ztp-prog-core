using System;
using ActorRouters.HOCON.Messages;
using Akka.Actor;

namespace ActorRouters.HOCON.Actors
{
    public class OrderActor : TypedActor, IHandle<OrderMessage>
    {
        static int _counter = 0;
        public int ActorId { get; } = ++_counter;

        public void Handle(OrderMessage message)
        {
            Console.WriteLine("Actor #{0} Receive message: {1} {2}", ActorId, message.Id, message.Name);
        }
    }
}