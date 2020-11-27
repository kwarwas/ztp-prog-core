using MediatR;

namespace CQRS.Core.Query
{
    public interface IQuery<out TQueryResult> : IRequest<TQueryResult>
    {
    }
}