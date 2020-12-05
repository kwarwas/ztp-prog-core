using CQRS.Core.Query;
using CQRS.Model.ReadModel;

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