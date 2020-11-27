using MediatR;

namespace CQRS.Core.Query
{
    public interface IQueryHandler<in TQuery, TQueryResult> : IRequestHandler<TQuery, TQueryResult>
        where TQuery : IQuery<TQueryResult>
    {
    }
}