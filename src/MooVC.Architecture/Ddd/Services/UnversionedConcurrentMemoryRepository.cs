namespace MooVC.Architecture.Ddd.Services;

using System;
using System.Collections.Generic;
using MooVC.Serialization;

public class UnversionedConcurrentMemoryRepository<TAggregate>
    : ConcurrentMemoryRepository<TAggregate>
    where TAggregate : AggregateRoot
{
    public UnversionedConcurrentMemoryRepository(ICloner cloner)
        : base(cloner)
    {
    }

    protected override Reference<TAggregate> GetKey(Guid id, Sequence? version)
    {
        return id.ToReference<TAggregate>();
    }

    protected override IEnumerable<TAggregate> PerformGetAll()
    {
        return Store.Values;
    }

    protected override Reference<TAggregate>? PerformGetCurrentVersion(TAggregate aggregate)
    {
        Reference<TAggregate>? key = GetKey(aggregate);

        return Store[key].ToReference();
    }
}