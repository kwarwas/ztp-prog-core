using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Core.Event
{
    public interface IEventsBus
    {
        Task Publish<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
            where TEvent : IEvent;
    }
}