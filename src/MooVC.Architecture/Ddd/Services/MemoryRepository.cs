namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using MooVC.Serialization;

    [Serializable]
    public class MemoryRepository<TAggregate>
        : Repository<TAggregate>,
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

        protected virtual IDictionary<Reference, TAggregate> Store { get; }

        public override TAggregate Get(Guid id, SignedVersion version = default)
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
                .Select(entry => entry.Value.Clone())
                .ToArray();
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Store), Store);
        }

        protected virtual TAggregate Get(Reference key)
        {
            _ = Store.TryGetValue(key, out TAggregate aggregate);

            return aggregate?.Clone();
        }

        protected override VersionedReference GetCurrentVersion(TAggregate aggregate)
        {
            Reference nonVersioned = aggregate.ToReference();

            _ = Store.TryGetValue(nonVersioned, out TAggregate existing);

            return existing?.ToVersionedReference();
        }

        protected virtual (Reference NonVersioned, Reference Versioned) GenerateReferences(TAggregate aggregate)
        {
            return (new Reference<TAggregate>(aggregate.Id), new VersionedReference<TAggregate>(aggregate.Id, aggregate.Version));
        }

        protected override void UpdateStore(TAggregate aggregate)
        {
            TAggregate copy = aggregate.Clone();

            copy.MarkChangesAsCommitted();

            (Reference nonVersioned, Reference versioned) = GenerateReferences(copy);

            _ = Store[nonVersioned] = copy;
            _ = Store[versioned] = copy;
        }
    }
}