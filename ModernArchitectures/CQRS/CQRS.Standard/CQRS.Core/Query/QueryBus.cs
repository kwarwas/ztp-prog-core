using System;
using System.Threading.Tasks;
using CQRS.Core.Common;

namespace CQRS.Core.Query
{
    public class QueryBus : IQueryBus
    {
        private readonly IHandlerRegistrar<IQueryHandler> _queryHandlerRegistrar;

        public QueryBus(IHandlerRegistrar<IQueryHandler> queryHandlerRegistrar)
        {
            _queryHandlerRegistrar = queryHandlerRegistrar;
        }

        public async Task<TResult> Query<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            var handler = _queryHandlerRegistrar.GetHandler<TQuery>();

            if (handler is IQueryHandler<TQuery, TResult> queryHandler)
            {
                return await queryHandler.Handle(query);
            }

            throw new ArgumentException();
        }
    }
}