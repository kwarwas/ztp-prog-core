using System.Threading.Tasks;
using Akka.Actor;
using CQRS.ReadSide.Database;
using Dapper;
using Pomelo.Data.MySql;

namespace CQRS.ReadSide.Query
{
    public class PersonHandler : ReceiveActor
    {
        private readonly string connectionString = @"Server=localhost;database=people;uid=root;pwd=password;";

        public PersonHandler()
        {
            ReceiveAsync<GetPersonList>(Handle);
            ReceiveAsync<GetPersonDetails>(Handle);
        }

        private async Task Handle(GetPersonList query)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                var result = await connection.QueryAsync<PersonListItemRecord>(
                    "select * from PersonList"
                );
                Sender.Tell(result, Self);
            }
        }

        private async Task Handle(GetPersonDetails query)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                var result = await connection.QuerySingleAsync<PersonDetailsRecord>(
                    "select * from PersonDetails where id = @id",
                    new {id = query.Id});

                Sender.Tell(result, Self);
            }
        }
    }
}