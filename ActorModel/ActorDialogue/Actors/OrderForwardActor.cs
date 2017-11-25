using ActorDialogue.Messages;
using ActorRouters.Actors;
using Akka.Actor;

namespace ActorDialogue.Actors
{
    public class OrderForwardActor : ReceiveActor
    {
        public OrderForwardActor()
        {
            Receive<OrderMessage>(message => Forward(message));
        }

        private void Forward(OrderMessage message)
        {
            var target = Context.ActorOf<OrderReceiveActor>();

            target.Forward(message);
        }
    }
}