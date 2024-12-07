namespace MooVC.Architecture.Ddd.Services;

using System;
using System.Collections.Generic;
using MooVC.Serialization;

public abstract class MemoryRepository<TAggregate, TStore>
    : SynchronousRepository<TAggregate>
    where TAggregate : AggregateRoot
    where TStore : IDictionary<Reference<TAggregate>, TAggregate>, new()
{
    protected MemoryRepository(ICloner cloner)
        : base(cloner)
    {
    }

    protected TStore Store { get; } = new TStore();

    protected virtual Reference<TAggregate> GetKey(TAggregate aggregate)
    {
        return GetKey(aggregate.Id, aggregate.Version);
    }

    protected abstract Reference<TAggregate> GetKey(Guid id, Sequence? version);

    protected override TAggregate? PerformGet(Guid id, Sequence? version = default)
    {
        Reference<TAggregate>? key = GetKey(id, version);

        if (Store.TryGetValue(key, out TAggregate? aggregate))
        {
            if (version is null || version.IsEmpty || version == aggregate.Version)
            {
                return aggregate;
            }
        }

        return default;
    }

    protected override void PerformUpdateStore(TAggregate aggregate)
    {
        aggregate.MarkChangesAsCommitted();

        Reference<TAggregate> key = GetKey(aggregate);

        _ = Store[key] = aggregate;
    }
}