using PageConstructor.Application.Common.EventBus.Brokers;
using PageConstructor.Domain.Common.Events;
using MediatR;

namespace PageConstructor.Infrastructure.Common.EventBus.Brokers;

public class RabbitMqEventBusBroker(IPublisher publisher) : IEventBusBroker
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
    await publisher.Publish(@event, cancellationToken);
}
