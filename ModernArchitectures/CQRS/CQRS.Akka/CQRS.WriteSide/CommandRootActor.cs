using Akka.Actor;
using CQRS.WriteSide.Commands;

namespace CQRS.WriteSide
{
    public class CommandRootActor : ReceiveActor
    {
        public CommandRootActor()
        {
            var saveAddressCommandHandler = Context.ActorOf<SaveAddressCommandHandler>();
            var savePersonCommandHandler = Context.ActorOf<SavePersonCommandHandler>();
            
            Receive<SavePerson>(message => savePersonCommandHandler.Forward(message));
            Receive<SaveAddress>(message => saveAddressCommandHandler.Forward(message));
        }
    }
}