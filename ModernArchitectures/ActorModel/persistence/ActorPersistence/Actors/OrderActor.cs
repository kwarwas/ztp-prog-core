using ActorPersistence.Messages;
using System;
using Akka.Persistence;
using System.Collections.Generic;
using System.Linq;

namespace ActorPersistence.Actors
{
    public class OrderActor : ReceivePersistentActor
    {
        public string OrderId { get; }
        public ICollection<OrderDetailsMessage> OrderDetails { get; }

        public override string PersistenceId { get; }
        
        public OrderActor(string orderId)
        {
            PersistenceId = $"Order_{orderId}";
            OrderId = orderId;
            OrderDetails = new List<OrderDetailsMessage>();

            Recover<OrderDetailsMessage>(message =>
            {
                Console.WriteLine("RECOVER message: {0} {1}", message.Name, message.Price);
                OrderDetails.Add(message);
            });

            Command<OrderDetailsMessage>(HandleOrderDetails);
            Command<PriceMessage>(HandlePrice);
            Command<ErrorMessage>(HandleError);
        }

        private void HandleOrderDetails(OrderDetailsMessage message)
        {
            Console.WriteLine("Receive message: {0} {1}", message.Name, message.Price);

            Persist(message, x =>
            {
                Console.WriteLine("PERSIST message: {0} {1}", message.Name, message.Price);
                OrderDetails.Add(x);
            });
        }

        private void HandlePrice(PriceMessage message)
        {
            Console.WriteLine("Total price: {0}", OrderDetails.Sum(x => x.Price));
        }

        private void HandleError(ErrorMessage message)
        {
            throw new Exception();
        }
    }
}