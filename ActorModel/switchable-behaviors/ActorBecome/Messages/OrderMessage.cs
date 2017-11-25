namespace ActorBecome.Messages
{
    public class OrderMessage
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public OrderMessage(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
