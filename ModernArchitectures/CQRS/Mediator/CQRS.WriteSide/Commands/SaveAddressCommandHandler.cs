using System.Threading;
using System.Threading.Tasks;
using CQRS.Core.Command;
using CQRS.Core.Event;
using CQRS.Model.WriteModel;
using CQRS.WriteSide.Database;
using CQRS.WriteSide.Events;
using MediatR;

namespace CQRS.WriteSide.Commands
{
    class SaveAddressCommandHandler: ICommandHandler<SaveAddress>
    {
        private readonly IEventsBus _eventsBus;

        public SaveAddressCommandHandler(IEventsBus eventsBus)
        {
            _eventsBus = eventsBus;
        }
        
        public async Task<Unit> Handle(SaveAddress saveAddress, CancellationToken cancellationToken = default)
        {
            await using var context = new MySqlDbContext();
            var record = new AddressRecord
            {
                City = saveAddress.City,
                PostalCode = saveAddress.PostalCode,
                Street = saveAddress.Street,
                Number = saveAddress.Number,
                AddressType = saveAddress.AddressType,
                PersonId = saveAddress.PersonId
            };
                
            await context.Addresses.AddAsync(record, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
                
            await _eventsBus.Publish(new AddressSaved(record.Id, saveAddress.PersonId), cancellationToken);
            
            return Unit.Value;
        }
    }
}