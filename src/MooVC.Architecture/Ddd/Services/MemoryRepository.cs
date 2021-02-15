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
            return (new Reference<TAggregate>(aggregate.Id),
                new VersionedReference<TAggregate>(aggregate.Id, aggregate.Version));
        }

        protected override TAggregate? PerformGet(Guid id, SignedVersion? version = default)
        {
            Reference key = version is { }
                ? (Reference)new VersionedReference<TAggregate>(id, version)
                : new Reference<TAggregate>(id);

            return Get(key);
        }

        protected override IEnumerable<TAggregate> PerformGetAll()
        {
            return Store
                .Where(entry => entry.Key is Reference<TAggregate>)
                .Select(entry => cloner(entry.Value))
                .ToArray();
        }

        protected override VersionedReference? PerformGetCurrentVersion(TAggregate aggregate)
        {
            Reference nonVersioned = aggregate.ToReference();

            if (Store.TryGetValue(nonVersioned, out TAggregate? existing))
            {
                return existing.ToVersionedReference();
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