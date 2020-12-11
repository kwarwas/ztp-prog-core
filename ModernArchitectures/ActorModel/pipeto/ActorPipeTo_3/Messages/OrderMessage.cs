namespace ActorPipeTo_3.Messages
{
    public class OrderMessage
    {
        public int Id { get; }
        public string Name { get; }

        public OrderMessage(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
