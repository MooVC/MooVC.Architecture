namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MooVC.Serialization;
    using static MooVC.Architecture.Ddd.Services.Resources;
    using static MooVC.Ensure;

    public class MemoryRepository<TAggregate>
        : Repository<TAggregate>
        where TAggregate : AggregateRoot
    {
        private readonly ICloner cloner;

        public MemoryRepository(ICloner cloner)
        {
            ArgumentNotNull(cloner, nameof(cloner), MemoryRepositoryClonerRequired);

            this.cloner = cloner;
        }

        protected virtual IDictionary<Reference, TAggregate> Store { get; } = new Dictionary<Reference, TAggregate>();

        public override TAggregate? Get(Guid id, SignedVersion? version = default)
        {
            Reference key = version is { }
                ? (Reference)new VersionedReference<TAggregate>(id, version)
                : new Reference<TAggregate>(id);

            return Get(key);
        }

        public override IEnumerable<TAggregate> GetAll()
        {
            return Store
                .Where(entry => entry.Key is Reference<TAggregate>)
                .Select(entry => cloner.Clone(entry.Value))
                .ToArray();
        }

        protected virtual TAggregate? Get(Reference key)
        {
            if (Store.TryGetValue(key, out TAggregate? aggregate))
            {
                return cloner.Clone(aggregate);
            }

            return default;
        }

        protected override VersionedReference? GetCurrentVersion(TAggregate aggregate)
        {
            Reference nonVersioned = aggregate.ToReference();

            if (Store.TryGetValue(nonVersioned, out TAggregate? existing))
            {
                return existing.ToVersionedReference();
            }

            return default;
        }

        protected virtual (Reference NonVersioned, Reference Versioned) GenerateReferences(TAggregate aggregate)
        {
            return (new Reference<TAggregate>(aggregate.Id), new VersionedReference<TAggregate>(aggregate.Id, aggregate.Version));
        }

        protected override void UpdateStore(TAggregate aggregate)
        {
            TAggregate copy = cloner.Clone(aggregate);

            copy.MarkChangesAsCommitted();

            (Reference nonVersioned, Reference versioned) = GenerateReferences(copy);

            _ = Store[nonVersioned] = copy;
            _ = Store[versioned] = copy;
        }
    }
}