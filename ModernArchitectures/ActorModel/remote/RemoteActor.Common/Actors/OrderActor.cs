using Akka.Actor;
using RemoteActor.Common.Messages;
using System;

namespace RemoteActor.Common.Actors
{
    public class OrderActor : ReceiveActor
    {
        public OrderActor()
        {
            Receive<OrderMessage>(message => Handle(message));
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