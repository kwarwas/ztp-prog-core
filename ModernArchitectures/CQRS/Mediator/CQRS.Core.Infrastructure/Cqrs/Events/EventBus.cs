using System.Threading;
using System.Threading.Tasks;
using CQRS.Core.Event;
using MediatR;

namespace CQRS.Core.Infrastructure.Cqrs.Events
{
    public class EventBus : IEventsBus
    {
        private readonly IMediator _mediator;

        public EventBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Publish<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
            where TEvent : IEvent
        {
            await _mediator.Publish(@event, cancellationToken);
        }
    }
}