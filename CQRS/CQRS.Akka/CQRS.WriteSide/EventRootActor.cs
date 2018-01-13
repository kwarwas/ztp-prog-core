using Akka.Actor;
using CQRS.WriteSide.Events;

namespace CQRS.WriteSide
{
    public class EventRootActor : ReceiveActor
    {
        public EventRootActor()
        {
            var personListEventsHandler = Context.ActorOf<PersonListEventsHandler>();
            var personDetailsEventHandler = Context.ActorOf<PersonDetailsEventHandler>();
            
            Receive<PersonSaved>(message =>
            {
                personListEventsHandler.Forward(message);
                personDetailsEventHandler.Forward(message);
            });
            Receive<AddressSaved>(message => personListEventsHandler.Forward(message));
        }
    }
}