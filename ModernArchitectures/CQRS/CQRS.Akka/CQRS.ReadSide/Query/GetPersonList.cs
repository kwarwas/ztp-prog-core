using System.Collections.Generic;
using CQRS.Core.Query;
using CQRS.ReadSide.Database;

namespace CQRS.ReadSide.Query
{
    public class GetPersonList : IQuery<IEnumerable<PersonListItemRecord>>
    {
    }
}