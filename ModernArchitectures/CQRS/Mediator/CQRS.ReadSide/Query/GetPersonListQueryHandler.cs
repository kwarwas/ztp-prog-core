using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Core.Query;
using CQRS.Model.ReadModel;
using MySql.Data.MySqlClient;
using RepoDb;

namespace CQRS.ReadSide.Query
{
    public class GetPersonListQueryHandler : IQueryHandler<GetPersonList, IEnumerable<PersonListItemRecord>>
    {
        public async Task<IEnumerable<PersonListItemRecord>> Handle(GetPersonList query,
            CancellationToken cancellationToken = default)
        {
            await using var connection = new MySqlConnection(Program.ConnectionString);
            return await connection.QueryAllAsync<PersonListItemRecord>(
                "PersonList",
                cancellationToken: cancellationToken);
        }
    }
}