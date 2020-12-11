using System.Collections.Immutable;

namespace ActorDialogue.Messages
{
    public class OrderMessage
    {
        public int Id { get; }
        public ImmutableArray<string> Names { get; }

        public OrderMessage(int id, string[] names)
        {
            Id = id;
            Names = names.ToImmutableArray();
        }
    }
}
