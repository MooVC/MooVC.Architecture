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
            Store = new Dictionary<SearchKey, TAggregate>();
        }

        protected MemoryRepository(SerializationInfo info, StreamingContext context)
        {
            Store = (Dictionary<SearchKey, TAggregate>)info.GetValue(nameof(Store), typeof(Dictionary<SearchKey, TAggregate>));
        }

        public event AggregateSavedEventHandler<TAggregate> AggregateSaved;

        public event AggregateSavingEventHandler<TAggregate> AggregateSaving;

        protected virtual IDictionary<SearchKey, TAggregate> Store { get; }

        public virtual TAggregate Get(Guid id, ulong? version = default)
        {
            var key = new SearchKey(id, version);

            return Get(key);
        }

        public virtual IEnumerable<TAggregate> GetAll()
        {
            return Store
                .Where(entry => entry.Key.IsLatest)
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

            (SearchKey NonVersioned, SearchKey Versioned) = GenerateReferences(copy);

            OnAggregateSaving(aggregate);

            PerformSave(copy, NonVersioned, Versioned);

            OnAggregateSaved(aggregate);

            aggregate.MarkChangesAsCommitted();
        }

        protected virtual TAggregate Get(SearchKey key)
        {
            _ = Store.TryGetValue(key, out TAggregate aggregate);

            return aggregate;
        }

        protected virtual (SearchKey NonVersioned, SearchKey Versioned) GenerateReferences(TAggregate aggregate)
        {
            return (new SearchKey(aggregate.Id), new SearchKey(aggregate.Id, aggregate.Version));
        }

        protected virtual void CheckForConflicts(TAggregate aggregate, SearchKey nonVersioned)
        {
            if (Store.TryGetValue(nonVersioned, out TAggregate existing))
            {
                AggregateDoesNotConflict<TAggregate>(aggregate, existing.Version);
            }
        }

        protected virtual void PerformSave(TAggregate aggregate, SearchKey nonVersioned, SearchKey versioned)
        {
            CheckForConflicts(aggregate, nonVersioned);
            UpdateStore(aggregate, nonVersioned, versioned);
        }

        protected virtual void UpdateStore(TAggregate aggregate, SearchKey nonVersioned, SearchKey versioned)
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

        [Serializable]
        protected class SearchKey
            : Value
        {
            public SearchKey(Guid id)
            {
                Id = id;
            }

            public SearchKey(Guid id, ulong? version)
            {
                Id = id;
                Version = version.GetValueOrDefault();
            }

            private SearchKey(SerializationInfo info, StreamingContext context) 
                : base(info, context)
            {
                Id = (Guid)info.GetValue(nameof(Id), typeof(Guid));
                Version = (ulong)info.GetValue(nameof(Version), typeof(ulong));
            }

            public Guid Id { get; }

            public ulong Version { get; }

            public bool IsLatest => Version == default;

            [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
            public override void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                base.GetObjectData(info, context);

                info.AddValue(nameof(Id), Id);
                info.AddValue(nameof(Version), Version);
            }

            protected override IEnumerable<object> GetAtomicValues()
            {
                yield return Id;
                yield return Version;
            }
        }
    }
}