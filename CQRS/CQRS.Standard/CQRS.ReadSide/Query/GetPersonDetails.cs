using System.Collections.Generic;
using CQRS.Core.Query;
using CQRS.ReadSide.Database;

namespace CQRS.ReadSide.Query
{
    public class GetPersonDetails : IQuery<PersonDetailsRecord>
    {
        public int Id { get; }

        public GetPersonDetails(int id)
        {
            Id = id;
        }
    }
}