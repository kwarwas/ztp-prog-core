using CQRS.Core.Event;

namespace CQRS.WriteSide.Events
{
    public class AddressSaved : IEvent
    {
        public int AddressId { get; }
        public int PersonId { get; }

        public AddressSaved(int addressId, int personId)
        {
            AddressId = addressId;
            PersonId = personId;
        }
    }
}