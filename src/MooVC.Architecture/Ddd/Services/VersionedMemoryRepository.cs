namespace MooVC.Architecture.Ddd.Services;

using System;
using System.Collections.Generic;
using MooVC.Serialization;

public class VersionedMemoryRepository<TAggregate>
    : MemoryRepository<TAggregate, Dictionary<Reference<TAggregate>, TAggregate>>
    where TAggregate : AggregateRoot
{
    public VersionedMemoryRepository(ICloner cloner)
        : base(cloner)
    {
    }

    protected override Reference<TAggregate> GetKey(Guid id, SignedVersion? version)
    {
        return Store.DetermineVersionedKey(id, version);
    }

    protected override IEnumerable<TAggregate> PerformGetAll()
    {
        return Store.GetLatestVersions();
    }

    protected override Reference<TAggregate>? PerformGetCurrentVersion(TAggregate aggregate)
    {
        return Store.FindLatestVersion(aggregate.Id);
    }
}