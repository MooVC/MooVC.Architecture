namespace MooVC.Architecture.Ddd;

public static partial class MessageExtensions
{
    public static bool TryIdentify<TAggregate>(this Message message, out Reference<TAggregate> aggregate)
        where TAggregate : AggregateRoot
    {
        aggregate = Reference<TAggregate>.Empty;

        if (message is Message<TAggregate> contextual)
        {
            aggregate = contextual.Aggregate;
        }
        else if (message is DomainEvent @event && @event.Aggregate.Is(out Reference<TAggregate>? source))
        {
            aggregate = source;
        }

        return !aggregate.IsEmpty;
    }
}