namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MooVC.Serialization;

    public sealed class MemoryRepository<TAggregate>
        : Repository<TAggregate>
        where TAggregate : AggregateRoot
    {
        private readonly HashSet<TAggregate> aggregates = new HashSet<TAggregate>();

        public override TAggregate Get(Guid id, ulong? version = null)
        {
            return aggregates
                .Where(aggregate => aggregate.Id == id 
                    && aggregate.Version == version.GetValueOrDefault(aggregate.Version))
                .OrderByDescending(aggregate => aggregate.Version)
                .FirstOrDefault();
        }

        public override IEnumerable<TAggregate> GetAll()
        {
            return aggregates
                .GroupBy(aggregate => aggregate.Id)
                .Select(aggregate => aggregate.OrderByDescending(version => version.Version).First())
                .ToArray();
        }

        protected override ulong GetCurrentVersion(Guid id)
        {
            return Get(id)?.Version ?? AggregateRoot.DefaultVersion;
        }

        protected override void PerformSave(TAggregate aggregate)
        {
            TAggregate existing = Get(aggregate.Id, version: aggregate.Version);

            if (existing != null)
            {
                throw new AggregateConflictDetectedException<TAggregate>(aggregate.Id, aggregate.Version - 1, existing.Version);
            }

            TAggregate copy = aggregate.Clone();

            _ = aggregates.Add(copy);
        }
    }
}