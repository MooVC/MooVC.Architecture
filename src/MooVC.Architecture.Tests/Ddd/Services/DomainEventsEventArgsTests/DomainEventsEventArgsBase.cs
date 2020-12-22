namespace MooVC.Architecture.Ddd.Services.DomainEventsEventArgsTests
{
    using System.Collections.Generic;
    using System.Linq;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Architecture.Ddd.DomainEventTests;
    using MooVC.Architecture.MessageTests;

    public abstract class DomainEventsEventArgsBase
    {
        protected IEnumerable<DomainEvent> CreateEvents(int count)
        {
            var context = new SerializableMessage();
            var aggregate = new SerializableAggregateRoot();
            var version = aggregate.ToVersionedReference();

            return Enumerable
                .Range(0, count)
                .Select(_ => new SerializableDomainEvent(context, version))
                .ToArray();
        }
    }
}