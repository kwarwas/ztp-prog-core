namespace CQRS.Core.Query
{
    public interface IQuery
    {
    }
    
    public interface IQuery<TResult> : IQuery
    {
    }
}