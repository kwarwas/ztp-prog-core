using System.Collections.Generic;
using System.Threading.Tasks;
using CQRS.Core.Query;
using CQRS.ReadSide.Database;
using Dapper;
using Pomelo.Data.MySql;

namespace CQRS.ReadSide.Query
{
    public class PersonHandler:
        IQueryHandler<GetPersonList, IEnumerable<PersonListItemRecord>>,
        IQueryHandler<GetPersonDetails, PersonDetailsRecord>
    {
        private readonly string connectionString = @"Server=localhost;database=people;uid=root;pwd=password;";
        
        public Task<IEnumerable<PersonListItemRecord>> Handle(GetPersonList query)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                return connection.QueryAsync<PersonListItemRecord>("select * from PersonList");
            }
        }

        public Task<PersonDetailsRecord> Handle(GetPersonDetails query)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                return connection.QuerySingleAsync<PersonDetailsRecord>(
                    "select * from PersonDetails where id = @id",
                    new {id = query.Id});
            }
        }
    }
}