using System.Threading.Tasks;
using CQRS.Core.Command;
using CQRS.Core.Event;
using CQRS.WriteSide.Database;
using CQRS.WriteSide.Database.WriteModel;
using CQRS.WriteSide.Events;

namespace CQRS.WriteSide.Commands
{
    class SaveAddressCommandHandler: ICommandHandler<SaveAddress>
    {
        private readonly IEventsBus _eventsBus;

        public SaveAddressCommandHandler(IEventsBus eventsBus)
        {
            _eventsBus = eventsBus;
        }
        
        public async Task Handle(SaveAddress saveAddress)
        {
            using (var context = new MySqlDbContext())
            {
                var record = new AddressRecord
                {
                    City = saveAddress.City,
                    PostalCode = saveAddress.PostalCode,
                    Street = saveAddress.Street,
                    Number = saveAddress.Number,
                    AddressType = saveAddress.AddressType,
                    PersonId = saveAddress.PersonId
                };
                
                await context.Addresses.AddAsync(record);
                await context.SaveChangesAsync();
                
                await _eventsBus.Publish(new AddressSaved(record.Id, saveAddress.PersonId));
            }
        }
    }
}