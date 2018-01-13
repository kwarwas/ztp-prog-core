using System.Collections.Generic;

namespace CQRS.Core.Common
{
    public interface IHandlerRegistrator<THandlerType>
    {
        void Register<TEvent>(THandlerType eventHandler);
        ICollection<THandlerType> GetHandlers<TEvent>();
        THandlerType GetHandler<TType>();
    }
}