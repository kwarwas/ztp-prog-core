using System.Collections.Generic;
using CQRS.Core.Query;
using CQRS.Model.ReadModel;

namespace CQRS.ReadSide.Query
{
    public class GetPersonList : IQuery<IEnumerable<PersonListItemRecord>>
    {
    }
}