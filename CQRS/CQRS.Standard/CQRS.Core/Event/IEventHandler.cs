using System.Threading.Tasks;

namespace CQRS.Core.Event
{
    public interface IEventHandler
    {
    }

    public interface IEventHandler<in TEvent> : IEventHandler where TEvent : IEvent
    {
        Task Handle(TEvent @event);
    }
}