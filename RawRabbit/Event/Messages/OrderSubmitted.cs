using System;

namespace Messages
{
    public class OrderSubmitted
    {
        public Guid Id { get; }
        public string Name { get; }
        public ushort Weight { get; }

        public OrderSubmitted(Guid id, string name, ushort weight)
        {
            Id = id;
            Name = name;
            Weight = weight;
        }

        public override string ToString()
        {
            return $"Id: {Id}\nName: {Name}\nWeight: {Weight}";
        }
    }
}