using System;
using System.Collections.Generic;
using System.Linq;
using ActorPersistenceCommandEvent.Commands;
using ActorPersistenceCommandEvent.Events;
using Akka.Persistence;

namespace ActorPersistenceCommandEvent.Actors
{
    public class OrderActor : ReceivePersistentActor
    {
        public string OrderId { get; }
        public ICollection<OrderDetailsAdded> OrderDetails { get; }

        public override string PersistenceId { get; }
        
        public OrderActor(string orderId)
        {
            PersistenceId = $"Order_{orderId}";
            OrderId = orderId;
            OrderDetails = new List<OrderDetailsAdded>();

            Recover<OrderDetailsAdded>(message =>
            {
                Console.WriteLine("RECOVER message: {0} {1}", message.Name, message.Price);
                OrderDetails.Add(message);
            });

            Command<AddOrderDetails>(command => HandleOrderDetails(command));
            Command<CalculatePrice>(command => HandlePrice(command));
            Command<ThrowError>(command => HandleError(command));
        }

        private void HandleOrderDetails(AddOrderDetails command)
        {
            Console.WriteLine("Receive message: {0} {1}", command.Name, command.Price);

            var orderDetails = new OrderDetailsAdded(command.Name, command.Price);

            Persist(orderDetails, x =>
            {
                Console.WriteLine("PERSIST message: {0} {1}", orderDetails.Name, orderDetails.Price);
                OrderDetails.Add(x);
            });
        }

        private void HandlePrice(CalculatePrice command)
        {
            Console.WriteLine("Total price: {0}", OrderDetails.Sum(x => x.Price));
        }

        private void HandleError(ThrowError message)
        {
            throw new Exception();
        }
    }
}