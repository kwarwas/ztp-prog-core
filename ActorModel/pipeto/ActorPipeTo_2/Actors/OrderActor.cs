using System;
using System.Threading.Tasks;
using ActorPipeTo_2.Messages;
using Akka.Actor;

namespace ActorPipeTo_2.Actors
{
    public class OrderActor : ReceiveActor
    {
        public OrderActor()
        {
            Receive<OrderMessage>(x => Handle(x));
        }

        public async void Handle(OrderMessage message)
        {
            Console.WriteLine("Receive message: {0}", message.Id);
            Console.WriteLine("Processing message: {0}", message.Id);
            await Process(message);
        }

        async Task Process(OrderMessage message)
        {
            await Task.Delay(TimeSpan.FromSeconds(1))
                .ContinueWith(x => Console.WriteLine("Message {0} processed", message.Id));
        }
    }
}