﻿using System.Linq;
using System.Threading.Tasks;
using CQRS.Core.Event;
using CQRS.WriteSide.Database;
using CQRS.WriteSide.Database.ReadModel;
using Microsoft.EntityFrameworkCore;

namespace CQRS.WriteSide.Events
{
    public class PersonListEventsHandler :
        IEventHandler<PersonSaved>,
        IEventHandler<AddressSaved>
    {
        public async Task Handle(PersonSaved @event)
        {
            using (var context = new MySqlDbContext())
            {
                var person = await context.People.FirstOrDefaultAsync(x => x.Id == @event.Id);

                var record = new PersonListItemRecord
                {
                    OriginalId = @event.Id,
                    FirstName = person.FirstName,
                    LastName = person.LastName
                };

                await context.PeopleList.AddAsync(record);
                await context.SaveChangesAsync();
            }
        }

        public async Task Handle(AddressSaved @event)
        {
            using (var context = new MySqlDbContext())
            {
                var personReadRecord = await context.PeopleList.FirstOrDefaultAsync(x => x.OriginalId == @event.PersonId);

                personReadRecord.AddressesCount = context.Addresses.Count(x => x.PersonId == @event.PersonId);

                await context.SaveChangesAsync();
            }
        }
    }
}