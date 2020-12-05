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
    class SavePersonCommandHandler : ICommandHandler<SavePerson>
    {
        private readonly IEventsBus _eventsBus;

        public SavePersonCommandHandler(IEventsBus eventsBus)
        {
            _eventsBus = eventsBus;
        }

        public async Task<Unit> Handle(SavePerson savePerson, CancellationToken cancellationToken = default)
        {
            var record = new PersonRecord
            {
                FirstName = savePerson.FirstName,
                LastName = savePerson.LastName
            };

            await using var context = new MySqlDbContext();
            await context.People.AddAsync(record, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            await _eventsBus.Publish(new PersonSaved(record.Id), cancellationToken);
            
            return Unit.Value;
        }
    }
}