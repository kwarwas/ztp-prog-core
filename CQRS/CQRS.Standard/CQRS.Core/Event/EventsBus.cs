using System.Threading.Tasks;
using CQRS.Core.Common;

namespace CQRS.Core.Event
{
    public class EventsBus : IEventsBus
    {
        private readonly IHandlerRegistrator<IEventHandler> _eventHandlerRegistrator;

        public EventsBus(IHandlerRegistrator<IEventHandler> eventHandlerRegistrator)
        {
            _eventHandlerRegistrator = eventHandlerRegistrator;
        }
        
        public async Task Publish<TEvent>(TEvent @event) where TEvent : IEvent
        {
            foreach (var handler in _eventHandlerRegistrator.GetHandlers<TEvent>())
            {
                if (handler is IEventHandler<TEvent> eventHandler)
                {
                    await eventHandler.Handle(@event);
                }
            }
        }
    }
}