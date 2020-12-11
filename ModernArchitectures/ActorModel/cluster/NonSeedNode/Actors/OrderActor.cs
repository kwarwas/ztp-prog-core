using System;
using Akka.Actor;
using NonSeedNode.Messages;

namespace NonSeedNode.Actors
{
    public class OrderActor : ReceiveActor
    {
        public OrderActor()
        {
            Receive<OrderMessage>(x => Handle(x));
        }

        private void Handle(OrderMessage message)
        {
            Console.WriteLine("Actor #{0} Receive message: {1} {2}", Self.Path, message.Id, message.Name);
        }

        protected override void PreStart()
        {
            Console.WriteLine("Actor has been created");
        }
    }
}