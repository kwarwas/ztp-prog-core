using System.Threading.Tasks;

namespace CQRS.Core.Event
{
    public interface IEventsBus
    {
        Task Publish<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}