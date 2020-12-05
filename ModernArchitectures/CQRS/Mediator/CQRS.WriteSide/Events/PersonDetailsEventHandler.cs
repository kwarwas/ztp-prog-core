using System.Threading;
using System.Threading.Tasks;
using CQRS.Core.Event;
using CQRS.Model.ReadModel;
using CQRS.WriteSide.Database;
using Microsoft.EntityFrameworkCore;

namespace CQRS.WriteSide.Events
{
    public class PersonDetailsEventHandler : IEventHandler<PersonSaved>
    {
        public async Task Handle(PersonSaved @event, CancellationToken cancellationToken = default)
        {
            await using var context = new MySqlDbContext();
            var person = await context.People.FirstOrDefaultAsync(
                x => x.Id == @event.Id, cancellationToken: cancellationToken);

            var record = new PersonDetailsRecord
            {
                FirstName = person.FirstName,
                LastName = person.LastName
            };

            await context.PeopleDetails.AddAsync(record, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}