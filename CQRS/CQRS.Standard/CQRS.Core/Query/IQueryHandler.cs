using System.Threading.Tasks;

namespace CQRS.Core.Query
{
    public interface IQueryHandler
    {
    }
    
    public interface IQueryHandler<in TQuery, TResult>: IQueryHandler 
        where TQuery: IQuery<TResult>
    {
        Task<TResult> Handle(TQuery query);
    }
}