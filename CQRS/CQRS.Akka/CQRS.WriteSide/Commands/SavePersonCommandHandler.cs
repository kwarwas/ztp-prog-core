using System.Threading.Tasks;
using Akka.Actor;
using CQRS.WriteSide.Database;
using CQRS.WriteSide.Database.WriteModel;
using CQRS.WriteSide.Events;

namespace CQRS.WriteSide.Commands
{
    class SavePersonCommandHandler : ReceiveActor
    {
        public SavePersonCommandHandler()
        {
            ReceiveAsync<SavePerson>(Handle);
        }

        private async Task Handle(SavePerson savePerson)
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
            
            Context.System.ActorSelection("*/EventRootActor").Tell(new PersonSaved(record.Id));

            Sender.Tell(new IdCommandResult(record.Id), Self);
        }
    }
}