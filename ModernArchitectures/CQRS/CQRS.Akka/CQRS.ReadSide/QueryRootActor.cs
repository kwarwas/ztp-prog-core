using Akka.Actor;
using CQRS.ReadSide.Query;

namespace CQRS.ReadSide
{
    public class QueryRootActor: ReceiveActor
    {
        public QueryRootActor()
        {
            var personHandlerProps = Props.Create<PersonHandler>();
            var personHandler = Context.ActorOf(personHandlerProps);

            Receive<GetPersonList>(message => personHandler.Forward(message));
            Receive<GetPersonDetails>(message => personHandler.Forward(message));
        }
    }
}