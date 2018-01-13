using System;
using System.Threading.Tasks;
using CQRS.Core.Common;

namespace CQRS.Core.Query
{
    public class QueryBus : IQueryBus
    {
        private readonly IHandlerRegistrator<IQueryHandler> _queryHandlerRegistrator;

        public QueryBus(IHandlerRegistrator<IQueryHandler> queryHandlerRegistrator)
        {
            _queryHandlerRegistrator = queryHandlerRegistrator;
        }

        public async Task<TResult> Query<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            var handler = _queryHandlerRegistrator.GetHandler<TQuery>();

            if (handler is IQueryHandler<TQuery, TResult> queryHandler)
            {
                return await queryHandler.Handle(query);
            }

            throw new ArgumentException();
        }
    }
}