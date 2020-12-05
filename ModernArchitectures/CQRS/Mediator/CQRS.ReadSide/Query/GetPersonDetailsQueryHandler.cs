using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Core.Query;
using CQRS.Model.ReadModel;
using MySql.Data.MySqlClient;
using RepoDb;

namespace CQRS.ReadSide.Query
{
    public class GetPersonDetailsQueryHandler : IQueryHandler<GetPersonDetails, PersonDetailsRecord>
    {
        public async Task<PersonDetailsRecord> Handle(GetPersonDetails request, CancellationToken cancellationToken)
        {
            await using var connection = new MySqlConnection(Program.ConnectionString);
            var query = await connection.QueryAsync<PersonDetailsRecord>(
                "PersonDetails",
                x => x.Id == request.Id,
                cancellationToken: cancellationToken);
            return query.FirstOrDefault();
        }
    }
}