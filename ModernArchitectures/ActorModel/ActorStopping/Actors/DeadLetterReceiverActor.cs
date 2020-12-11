using Akka.Actor;
using Akka.Event;
using System;

namespace ActorStopping.Actors
{
    class DeadLetterReceiverActor : ReceiveActor
    {
        public DeadLetterReceiverActor()
        {
            Receive<DeadLetter>(message => Console.WriteLine("Receive message: {0} {1}", message.Message.GetType(), message.Sender));
        }
    }
}
