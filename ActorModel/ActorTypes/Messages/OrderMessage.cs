namespace ActorTypes.Messages
{
    public class OrderMessage
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public OrderMessage(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
