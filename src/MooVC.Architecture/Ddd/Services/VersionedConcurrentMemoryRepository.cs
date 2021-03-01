namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Collections.Generic;
    using MooVC.Serialization;
    using static MooVC.Architecture.Ddd.Services.Ensure;

    public class VersionedConcurrentMemoryRepository<TAggregate>
        : ConcurrentMemoryRepository<TAggregate>
        where TAggregate : AggregateRoot
    {
        public VersionedConcurrentMemoryRepository(ICloner? cloner = default)
            : base(cloner: cloner)
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

        protected override TAggregate PerformAdd(Reference<TAggregate> key, TAggregate proposed)
        {
            return PerformUpdate(proposed, key, proposed);
        }

        protected override TAggregate PerformUpdate(TAggregate existing, Reference<TAggregate> key, TAggregate proposed)
        {
            Reference<TAggregate>? current = PerformGetCurrentVersion(proposed);

            AggregateDoesNotConflict(proposed, currentVersion: current?.Version);

            return existing;
        }
    }
}