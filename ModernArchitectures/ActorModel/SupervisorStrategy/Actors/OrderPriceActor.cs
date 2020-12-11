using System;
using Akka.Actor;

namespace ActorSupervisorStrategy.Actors
{
    public class OrderPriceActor : ReceiveActor
    {
        public Guid Guid { get; } = Guid.NewGuid();

        public OrderPriceActor()
        {
            Receive<string>(message => GetPrice(message));
        }

        private void GetPrice(string message)
        {
            Console.WriteLine("{0} {1}", Guid, message);

            if (message.Length == 4)
            {
                throw new ArithmeticException();
//                throw new NotSupportedException();
//                throw new Exception();
            }

            Console.WriteLine("ok");

            Sender.Tell((decimal)message.Length, Self);
        }

        protected override void PreStart()
        {
            Console.WriteLine("PreStart");
            base.PreStart();
        }

        protected override void PostStop()
        {
            Console.WriteLine("PostStop");
            base.PostStop();
        }

        protected override void PreRestart(Exception reason, object message)
        {
            Console.WriteLine(reason.Message);
            base.PreRestart(reason, message);
        }
    }
}