using System.Threading.Tasks;
using CQRS.Core.Command;
using CQRS.Core.Event;
using CQRS.WriteSide.Database;
using CQRS.WriteSide.Database.WriteModel;
using CQRS.WriteSide.Events;

namespace CQRS.WriteSide.Commands
{
    class SavePersonCommandHandler : ICommandHandler<SavePerson>
    {
        private readonly IEventsBus _eventsBus;

        public SavePersonCommandHandler(IEventsBus eventsBus)
        {
            _eventsBus = eventsBus;
        }

        public async Task Handle(SavePerson savePerson)
        {
            var record = new PersonRecord
            {
                FirstName = savePerson.FirstName,
                LastName = savePerson.LastName
            };

            using (var context = new MySqlDbContext())
            {
                await context.People.AddAsync(record);
                await context.SaveChangesAsync();
            }

            await _eventsBus.Publish(new PersonSaved(record.Id));
        }
    }
}