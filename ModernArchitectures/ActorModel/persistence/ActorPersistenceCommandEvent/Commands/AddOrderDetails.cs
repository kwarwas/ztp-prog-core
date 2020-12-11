namespace ActorPersistenceCommandEvent.Commands
{
    public class AddOrderDetails
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public AddOrderDetails(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }
}
