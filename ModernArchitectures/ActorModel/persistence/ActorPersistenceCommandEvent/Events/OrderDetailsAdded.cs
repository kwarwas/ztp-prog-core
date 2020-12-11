using System;

namespace ActorPersistenceCommandEvent.Events
{
    public class OrderDetailsAdded
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public DateTime CreatedOn { get; private set; }

        public OrderDetailsAdded(string name, decimal price)
        {
            Name = name;
            Price = price;
            CreatedOn = DateTime.Now;
        }
    }
}
