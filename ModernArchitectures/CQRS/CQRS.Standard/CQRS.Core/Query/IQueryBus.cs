using System.Threading.Tasks;

namespace CQRS.Core.Query
{
    public interface IQueryBus
    {
        Task<TResult> Query<TQuery, TResult>(TQuery command) where TQuery: IQuery<TResult>;
    }
}