using System;
using Akka.Actor;
using RemoteActor.Common.Messages;

namespace RemoteActor.Remote.Actors
{
    public class OrderActor : ReceiveActor
    {
        public OrderActor()
        {
            Receive<OrderMessage>(Handle);
        }

        private void Handle(OrderMessage message)
        {
            Console.WriteLine("Receive message: {0} {1}", message.Id, message.Name);
            
            Sender.Tell(message.Id, Self);
        }

        protected override void PreStart()
        {
            Console.WriteLine("Creating actor");
        }
    }
}