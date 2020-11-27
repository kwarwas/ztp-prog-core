
using CQRS.Core.Command;
using CQRS.WriteSide.Database.WriteModel;

namespace CQRS.WriteSide.Commands
{
    class SaveAddress : ICommand
    {
        public int PersonId { get; }
        public string City { get; }
        public string PostalCode { get; }
        public string Street { get; }
        public string Number { get; }
        public AddressType AddressType { get; }

        public SaveAddress(int personId, string city, string postalCode, string street, string number,
            AddressType addressType = AddressType.Additional)
        {
            PersonId = personId;
            City = city;
            PostalCode = postalCode;
            Street = street;
            Number = number;
            AddressType = addressType;
        }
    }
}