using System;
using System.Collections.Generic;
using System.Linq;

namespace CQRS.Core.Common
{
    public class HandlerRegistrator<THandlerType> : IHandlerRegistrator<THandlerType>
    {
        private readonly Dictionary<Type, IList<THandlerType>> _handlerDictionary =
            new Dictionary<Type, IList<THandlerType>>();

        public void Register<TResultType>(THandlerType eventHandler)
        {
            var resultType = typeof(TResultType);
            
            if (!_handlerDictionary.ContainsKey(resultType))
            {
                _handlerDictionary[resultType] = new List<THandlerType>();
            }
            _handlerDictionary[resultType].Add(eventHandler);
        }
    
        public ICollection<THandlerType> GetHandlers<TResultType>()
        {
            return _handlerDictionary[typeof(TResultType)] ?? new List<THandlerType>();
        }
        
        public THandlerType GetHandler<TResultType>()
        {
            return GetHandlers<TResultType>().SingleOrDefault();
        }
    }
}