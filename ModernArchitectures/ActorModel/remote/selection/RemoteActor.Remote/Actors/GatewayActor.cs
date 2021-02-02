using Akka.Actor;
using RemoteActor.Common.Messages;

namespace RemoteActor.Remote.Actors
{
    public class GatewayActor : ReceiveActor
    {
        private readonly IActorRef _orderActor = Context.ActorOf<OrderActor>();
        
        public GatewayActor()
        {
            Receive<OrderMessage>(_orderActor.Forward);
        }
    }
}