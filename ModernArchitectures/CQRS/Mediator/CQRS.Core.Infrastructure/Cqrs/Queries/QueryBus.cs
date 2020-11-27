using System.Threading;
using System.Threading.Tasks;
using CQRS.Core.Query;
using MediatR;

namespace CQRS.Core.Infrastructure.Cqrs.Queries
{
    public class QueryBus : IQueryBus
    {
        private readonly IMediator _mediator;

        public QueryBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<TQueryResult> Send<TQuery, TQueryResult>(TQuery request,
            CancellationToken cancellationToken = default)
            where TQuery : IQuery<TQueryResult>
        {
            return await _mediator.Send(request, cancellationToken);
        }
    }
}