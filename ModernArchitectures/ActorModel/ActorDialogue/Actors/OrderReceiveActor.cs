using Akka.Actor;
using System;
using System.Threading.Tasks;
using System.Linq;
using ActorDialogue.Actors;
using ActorDialogue.Messages;

namespace ActorRouters.Actors
{
    public class OrderReceiveActor : ReceiveActor
    {
        public OrderReceiveActor()
        {
            ReceiveAsync<OrderMessage>(CalculateTotalPrice);
        }

        private async Task CalculateTotalPrice(OrderMessage message)
        {
            var tasks = message.Names
                .Select(x => Context.ActorOf<OrderPriceActor>().Ask<decimal>(x))
                .ToArray();

            await Task.WhenAll(tasks);

            Console.WriteLine("Total price: ${0}", tasks.Sum(x => x.Result));
        }
    }
}