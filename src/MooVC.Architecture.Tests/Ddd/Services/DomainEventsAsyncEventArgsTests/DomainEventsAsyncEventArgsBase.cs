namespace MooVC.Architecture.Ddd.Services.DomainEventsAsyncEventArgsTests;

using System.Collections.Generic;
using System.Linq;
using MooVC.Architecture.Ddd.DomainEventTests;
using MooVC.Architecture.MessageTests;

public abstract class DomainEventsAsyncEventArgsBase
{
    protected static IEnumerable<DomainEvent> CreateEvents(int count)
    {
        var context = new SerializableMessage();
        var aggregate = new SerializableAggregateRoot();

        return Enumerable
            .Range(0, count)
            .Select(_ => new SerializableDomainEvent<SerializableAggregateRoot>(aggregate, context))
            .ToArray();
    }
}