using System;
using ActorSupervisorStrategy.Messages;
using Akka.Actor;

namespace ActorSupervisorStrategy.Actors
{
    public class OrderReceiveActor : ReceiveActor
    {
        readonly IActorRef _actor = Context.ActorOf<OrderPriceActor>();

        public OrderReceiveActor()
        {
            Receive<OrderMessage>(message => _actor.Tell(message.Name));
        }

//        protected override SupervisorStrategy SupervisorStrategy()
//        {
//            return new OneForOneStrategy( //or AllForOneStrategy
//             maxNrOfRetries: 10,
//             withinTimeRange: TimeSpan.FromSeconds(30),
//             decider: Decider.From(x =>
//             {
//                 //Maybe we consider ArithmeticException to not be application critical
//                 //so we just ignore the error and keep going.
//                 switch (x)
//                 {
//                     case ArithmeticException _:
//                         Console.WriteLine("Resume");
//                         return Directive.Resume;
//                     case NotSupportedException _:
//                         return Directive.Stop;
//                     default:
//                         return Directive.Restart;
//                 }
//             }));
//        }
    }
}