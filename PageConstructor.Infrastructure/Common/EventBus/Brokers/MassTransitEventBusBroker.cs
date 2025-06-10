using PageConstructor.Application.Common.EventBus.Brokers;
using PageConstructor.Domain.Common.Events;
using MassTransit;
using MediatR;

namespace PageConstructor.Infrastructure.Common.EventBus.Brokers;

public class MassTransitEventBusBroker(IBus bus, IPublisher publisher) : IEventBusBroker
{
    public async ValueTask PublishAsync<TEvent>(
        TEvent @event,
        CancellationToken cancellationToken = default)
        where TEvent : EventBase =>
        await publisher.Publish(@event, cancellationToken);

    public async ValueTask PublishLocalAsync<TEvent>(
        TEvent @event,
        CancellationToken cancellationToken = default)
        where TEvent : EventBase =>
        await bus.Publish(@event, cancellationToken);
}
