namespace ActorPersistence.Messages
{
    public class OrderDetailsMessage
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public OrderDetailsMessage(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }
}
