using System.Threading.Tasks;
using CQRS.Core.Common;

namespace CQRS.Core.Event
{
    public class EventsBus : IEventsBus
    {
        private readonly IHandlerRegistrar<IEventHandler> _eventHandlerRegistrar;

        public EventsBus(IHandlerRegistrar<IEventHandler> eventHandlerRegistrar)
        {
            _eventHandlerRegistrar = eventHandlerRegistrar;
        }
        
        public async Task Publish<TEvent>(TEvent @event) where TEvent : IEvent
        {
            foreach (var handler in _eventHandlerRegistrar.GetHandlers<TEvent>())
            {
                if (handler is IEventHandler<TEvent> eventHandler)
                {
                    await eventHandler.Handle(@event);
                }
            }
        }
    }
}