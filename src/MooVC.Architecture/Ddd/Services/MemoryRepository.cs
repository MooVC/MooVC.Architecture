namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MooVC.Serialization;

    public class MemoryRepository<TAggregate>
        : SynchronousRepository<TAggregate>
        where TAggregate : AggregateRoot
    {
        private readonly Func<TAggregate, TAggregate> cloner;

        public MemoryRepository(ICloner? cloner = default)
        {
            this.cloner = cloner is { }
                ? cloner.Clone
                : SerializableExtensions.Clone;
        }

        protected virtual IDictionary<Reference, TAggregate> Store { get; } = new Dictionary<Reference, TAggregate>();

        protected virtual TAggregate? Get(Reference key)
        {
            if (Store.TryGetValue(key, out TAggregate? aggregate))
            {
                return cloner(aggregate);
            }

            return default;
        }

        protected virtual (Reference NonVersioned, Reference Versioned) GenerateReferences(TAggregate aggregate)
        {
            return (new Reference<TAggregate>(aggregate.Id, version: SignedVersion.Empty),
                new Reference<TAggregate>(aggregate.Id, version: aggregate.Version));
        }

        protected override TAggregate? PerformGet(Guid id, SignedVersion? version = default)
        {
            return Get(new Reference<TAggregate>(id, version: version));
        }

        protected override IEnumerable<TAggregate> PerformGetAll()
        {
            return Store
                .Where(entry => !entry.Key.IsVersioned)
                .Select(entry => cloner(entry.Value))
                .ToArray();
        }

        protected override Reference? PerformGetCurrentVersion(TAggregate aggregate)
        {
            Reference nonVersioned = new Reference<TAggregate>(aggregate.Id);

            if (Store.TryGetValue(nonVersioned, out TAggregate? existing))
            {
                return existing.ToReference();
            }

            return default;
        }

        protected override void PerformUpdateStore(TAggregate aggregate)
        {
            TAggregate copy = cloner(aggregate);

            copy.MarkChangesAsCommitted();

            (Reference nonVersioned, Reference versioned) = GenerateReferences(copy);

            _ = Store[nonVersioned] = copy;
            _ = Store[versioned] = copy;
        }
    }
}