namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MooVC.Serialization;
    using static Ensure;

    public class MemoryRepository<TAggregate>
        : IRepository<TAggregate>
        where TAggregate : AggregateRoot
    {
        public MemoryRepository()
        {
            Store = new Dictionary<Reference<TAggregate>, TAggregate>();
        }

        protected virtual IDictionary<Reference<TAggregate>, TAggregate> Store { get; }

        public virtual TAggregate Get(Guid id, ulong? version = null)
        {
            var reference = new Reference<TAggregate>(id, version: version);

            return Get(reference);
        }

        public virtual IEnumerable<TAggregate> GetAll()
        {
            return Store
                .Where(entry => !entry.Key.IsVersionSpecific)
                .Select(entry => entry.Value)
                .ToArray();
        }

        public virtual void Save(TAggregate aggregate)
        {
            TAggregate copy = aggregate.Clone();

            copy.MarkChangesAsCommitted();

            (Reference<TAggregate> NonVersioned, Reference<TAggregate> Versioned) = GenerateReferences(copy);

            PerformSave(copy, NonVersioned, Versioned);

            aggregate.MarkChangesAsCommitted();
        }

        protected virtual TAggregate Get(Reference<TAggregate> reference)
        {
            _ = Store.TryGetValue(reference, out TAggregate aggregate);

            return aggregate;
        }

        protected virtual (Reference<TAggregate> NonVersioned, Reference<TAggregate> Versioned) GenerateReferences(TAggregate aggregate)
        {
            return (aggregate.ToReference(), aggregate.ToReference(enforceVersion: true));
        }

        protected virtual void CheckForConflicts(TAggregate aggregate, Reference<TAggregate> nonVersioned)
        {
            if (Store.TryGetValue(nonVersioned, out TAggregate existing))
            {
                AggregateDoesNotConflict<TAggregate>(aggregate, existing.Version);
            }
        }

        protected virtual void PerformSave(TAggregate aggregate, Reference<TAggregate> nonVersioned, Reference<TAggregate> versioned)
        {
            CheckForConflicts(aggregate, nonVersioned);
            UpdateStore(aggregate, nonVersioned, versioned);
        }

        protected virtual void UpdateStore(TAggregate aggregate, Reference<TAggregate> nonVersioned, Reference<TAggregate> versioned)
        {
            _ = Store[nonVersioned] = aggregate;
            _ = Store[versioned] = aggregate;
        }
    }
}