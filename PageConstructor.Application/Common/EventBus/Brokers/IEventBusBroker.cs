using PageConstructor.Domain.Common.Events;

namespace PageConstructor.Application.Common.EventBus.Brokers;

public interface IEventBusBroker
{
    ValueTask PublishLocalAsync<TEvent>(
        TEvent @event,
        CancellationToken cancellationToken = default)
        where TEvent : EventBase;

    ValueTask PublishAsync<TEvent>(
        TEvent @event,
        CancellationToken cancellationToken = default)
        where TEvent : EventBase;
}
