namespace ActorPersistenceSnapshot.Commands
{
    public class AddOrderDetails
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public AddOrderDetails(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }
}
