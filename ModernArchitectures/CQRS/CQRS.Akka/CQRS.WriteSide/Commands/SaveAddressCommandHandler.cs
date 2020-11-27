using System.Threading.Tasks;
using Akka.Actor;
using CQRS.Core.Command;
using CQRS.WriteSide.Database;
using CQRS.WriteSide.Database.WriteModel;
using CQRS.WriteSide.Events;

namespace CQRS.WriteSide.Commands
{
    class SaveAddressCommandHandler : ReceiveActor
    {
        public SaveAddressCommandHandler()
        {
            ReceiveAsync<SaveAddress>(Handle);
        }

        private async Task Handle(SaveAddress saveAddress)
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

                Context.System.ActorSelection("*/EventRootActor")
                    .Tell(new AddressSaved(record.Id, saveAddress.PersonId));

                Sender.Tell(new CommandResult(), Self);
            }
        }
    }
}