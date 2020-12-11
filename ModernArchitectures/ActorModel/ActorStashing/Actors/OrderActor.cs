using Akka.Actor;
using ActorStashing.Messages;
using System;

namespace ActorStashing.Actors
{
    public class OrderActor : ReceiveActor, IWithUnboundedStash
    {
        private readonly ExternalServiceSimulator _simulator = new ExternalServiceSimulator();

        public IStash Stash { get; set; }

        public OrderActor()
        {
            Receive<OrderMessage>(message => Handle(message));
        }

        private void Handle(OrderMessage message)
        {
            Console.WriteLine("Receive message: {0} {1}", message.Id, message.Name);

            if (_simulator.IsBusy)
            {
                Console.WriteLine("STASHING message");
                Stash.Stash();
            }
            else
            {
                Stash.Unstash();
                Console.WriteLine("UNSTASHED all messages");
                _simulator.Proccess(message);
                Console.WriteLine("Message {0} processed", message.Id);
            }
        }
    }
}