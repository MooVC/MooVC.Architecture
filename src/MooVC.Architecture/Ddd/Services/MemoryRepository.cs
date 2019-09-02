namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using MooVC.Serialization;
    using static Ensure;

    [Serializable]
    public class MemoryRepository<TAggregate>
        : IRepository<TAggregate>, 
          ISerializable
        where TAggregate : AggregateRoot
    {
        public MemoryRepository()
        {
            Store = new Dictionary<Reference, TAggregate>();
        }

        protected MemoryRepository(SerializationInfo info, StreamingContext context)
        {
            Store = (Dictionary<Reference, TAggregate>)info.GetValue(nameof(Store), typeof(Dictionary<Reference, TAggregate>));
        }

        public event AggregateSavedEventHandler<TAggregate> AggregateSaved;

        public event AggregateSavingEventHandler<TAggregate> AggregateSaving;

        protected virtual IDictionary<Reference, TAggregate> Store { get; }

        public virtual TAggregate Get(Guid id, ulong? version = default)
        {
            Reference key = version.HasValue 
                ? new VersionedReference<TAggregate>(id, version.Value) 
                : (Reference)new Reference<TAggregate>(id);

            return Get(key);
        }

        public virtual IEnumerable<TAggregate> GetAll()
        {
            return Store
                .Where(entry => entry.Key is Reference<TAggregate>)
                .Select(entry => entry.Value)
                .ToArray();
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Store), Store);
        }

        public virtual void Save(TAggregate aggregate)
        {
            TAggregate copy = aggregate.Clone();

            copy.MarkChangesAsCommitted();

            (Reference NonVersioned, Reference Versioned) = GenerateReferences(copy);

            OnAggregateSaving(aggregate);

            PerformSave(copy, NonVersioned, Versioned);

            OnAggregateSaved(aggregate);

            aggregate.MarkChangesAsCommitted();
        }

        protected virtual TAggregate Get(Reference key)
        {
            _ = Store.TryGetValue(key, out TAggregate aggregate);

            return aggregate;
        }

        protected virtual (Reference NonVersioned, Reference Versioned) GenerateReferences(TAggregate aggregate)
        {
            return (new Reference<TAggregate>(aggregate.Id), new VersionedReference<TAggregate>(aggregate.Id, aggregate.Version));
        }

        protected virtual void CheckForConflicts(TAggregate aggregate, Reference nonVersioned)
        {
            if (Store.TryGetValue(nonVersioned, out TAggregate existing))
            {
                AggregateDoesNotConflict<TAggregate>(aggregate, existing.Version);
            }
        }

        protected virtual void PerformSave(TAggregate aggregate, Reference nonVersioned, Reference versioned)
        {
            CheckForConflicts(aggregate, nonVersioned);
            UpdateStore(aggregate, nonVersioned, versioned);
        }

        protected virtual void UpdateStore(TAggregate aggregate, Reference nonVersioned, Reference versioned)
        {
            _ = Store[nonVersioned] = aggregate;
            _ = Store[versioned] = aggregate;
        }

        protected void OnAggregateSaved(TAggregate aggregate)
        {
            AggregateSaved?.Invoke(this, new AggregateSavedEventArgs<TAggregate>(aggregate));
        }

        protected void OnAggregateSaving(TAggregate aggregate)
        {
            AggregateSaving?.Invoke(this, new AggregateSavingEventArgs<TAggregate>(aggregate));
        }
    }
}