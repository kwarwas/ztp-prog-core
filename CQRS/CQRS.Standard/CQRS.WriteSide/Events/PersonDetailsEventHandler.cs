using System.Threading.Tasks;
using CQRS.Core.Event;
using CQRS.WriteSide.Database;
using CQRS.WriteSide.Database.ReadModel;
using Microsoft.EntityFrameworkCore;

namespace CQRS.WriteSide.Events
{
    public class PersonDetailsEventHandler: IEventHandler<PersonSaved>
    {
        public async Task Handle(PersonSaved @event)
        {
            using (var context = new MySqlDbContext())
            {
                var person = await context.People.FirstOrDefaultAsync(x => x.Id == @event.Id);
                
                var record = new PersonDetailsRecord
                {
                    FirstName = person.FirstName,
                    LastName = person.LastName
                };
                
                await context.PeopleDetails.AddAsync(record);
                await context.SaveChangesAsync();
            }
        }
    }
}