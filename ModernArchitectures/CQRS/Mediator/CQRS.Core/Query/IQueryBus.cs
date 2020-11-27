using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Core.Query
{
    public interface IQueryBus
    {
        Task<TQueryResult> Send<TQuery, TQueryResult>(TQuery request, CancellationToken cancellationToken = default)
            where TQuery : IQuery<TQueryResult>;
    }
}