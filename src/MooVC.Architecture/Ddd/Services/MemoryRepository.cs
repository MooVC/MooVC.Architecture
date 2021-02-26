namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Collections.Generic;
    using MooVC.Serialization;

    public abstract class MemoryRepository<TAggregate, TStore>
        : SynchronousRepository<TAggregate>
        where TAggregate : AggregateRoot
        where TStore : IDictionary<Reference<TAggregate>, TAggregate>, new()
    {
        protected MemoryRepository(ICloner? cloner = default)
        {
            Cloner = cloner is { }
                ? cloner.Clone
                : SerializableExtensions.Clone;
        }

        protected Func<TAggregate, TAggregate> Cloner { get; }

        protected TStore Store { get; } = new TStore();

        protected virtual Reference<TAggregate> GetKey(TAggregate aggregate)
        {
            return GetKey(aggregate.Id, aggregate.Version);
        }

        protected abstract Reference<TAggregate> GetKey(Guid id, SignedVersion? version);

        protected override TAggregate? PerformGet(Guid id, SignedVersion? version = default)
        {
            Reference<TAggregate>? key = GetKey(id, version);

            if (Store.TryGetValue(key, out TAggregate? aggregate))
            {
                if (version is null || version.IsEmpty || version == aggregate.Version)
                {
                    return Cloner(aggregate);
                }
            }

            return default;
        }

        protected override void PerformUpdateStore(TAggregate aggregate)
        {
            TAggregate copy = Cloner(aggregate);

            copy.MarkChangesAsCommitted();

            Reference<TAggregate> key = GetKey(copy);

            _ = Store[key] = copy;
        }
    }
}