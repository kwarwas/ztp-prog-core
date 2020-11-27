using CQRS.Core.Event;

namespace CQRS.WriteSide.Events
{
    public class PersonSaved : IEvent
    {
        public int Id { get; }

        public PersonSaved(int id)
        {
            Id = id;
        }
    }
}