using Akka.Actor;
using ActorStacked.Messages;
using System;

namespace ActorStacked.Actors
{
    public class MarketReceiveActor : ReceiveActor
    {
        public MarketReceiveActor()
        {
            MarketClose();
        }

        private void MarketOpen()
        {
            Receive<MarketOpenMessage>(message => Console.WriteLine("Error - market is already open"));
            Receive<MarketCloseMessage>(message => MarketCloseHandle());
            Receive<OrderMessage>(message => Console.WriteLine("Receive message: {0} {1}", message.Id, message.Name));
        }

        private void MarketClose()
        {
            Receive<MarketOpenMessage>(message => MarketOpenHandle());
            Receive<MarketCloseMessage>(message => Console.WriteLine("Error - market already closed"));
            Receive<OrderMessage>(message => Console.WriteLine("Error - market is close"));
        }

        private void MarketOpenHandle()
        {
            BecomeStacked(MarketOpen);
            Console.WriteLine("Now market is OPEN");
        }

        private void MarketCloseHandle()
        {
            UnbecomeStacked();
            Console.WriteLine("Now market is CLOSE");
        }
    }
}