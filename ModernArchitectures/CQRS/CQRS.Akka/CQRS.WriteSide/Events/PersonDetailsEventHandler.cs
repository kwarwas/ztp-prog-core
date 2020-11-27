using System;
using System.Threading.Tasks;
using Akka.Actor;
using CQRS.WriteSide.Database;
using CQRS.WriteSide.Database.ReadModel;
using Microsoft.EntityFrameworkCore;

namespace CQRS.WriteSide.Events
{
    public class PersonDetailsEventHandler: ReceiveActor
    {
        public PersonDetailsEventHandler()
        {
            ReceiveAsync<PersonSaved>(Handle);
        }

        private async Task Handle(PersonSaved @event)
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