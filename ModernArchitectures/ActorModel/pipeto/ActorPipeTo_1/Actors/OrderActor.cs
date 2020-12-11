using System;
using System.Threading.Tasks;
using ActorPipeTo_1.Messages;
using Akka.Actor;

namespace ActorPipeTo_1.Actors
{
    public class OrderActor : ReceiveActor
    {
        public OrderActor()
        {
            Receive<OrderMessage>(x => Handle(x));
        }

        public void Handle(OrderMessage message)
        {
            Console.WriteLine("Receive message: {0}", message.Id);
            Console.WriteLine("Processing message: {0}", message.Id);
            Process(message);
        }

        void Process(OrderMessage message)
        {
            Task.Delay(TimeSpan.FromSeconds(1))
                .ContinueWith(x => Console.WriteLine("Message {0} processed", message.Id))
                .Wait();
        }
    }
}