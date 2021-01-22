using ActorDialogue.Messages;
using Akka.Actor;

namespace ActorDialogue.Actors
{
    public class OrderForwardActor : ReceiveActor
    {
        public OrderForwardActor()
        {
            Receive<OrderMessage>(Forward);
        }

        private void Forward(OrderMessage message)
        {
            var target = Context.ActorOf<OrderReceiveActor>();

            target.Forward(message);
        }
    }
}