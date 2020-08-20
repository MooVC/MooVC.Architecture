namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using MooVC.Architecture.Serialization;
    using static MooVC.Architecture.Ddd.Ensure;
    using static Resources;

    [Serializable]
    public abstract class Projection<TAggregate>
        : ISerializable
        where TAggregate : AggregateRoot
    {
        protected Projection(TAggregate aggregate)
            : this(aggregate.ToVersionedReference())
        {
        }

        protected Projection(VersionedReference<TAggregate> aggregate)
        {
            ReferenceIsNotEmpty(aggregate, nameof(aggregate), ProjectionAggregateRequired);

            Aggregate = aggregate;
        }

        protected Projection(SerializationInfo info, StreamingContext context)
        {
            Aggregate = info.TryGetVersionedReference<TAggregate>(nameof(Aggregate));
        }

        public VersionedReference<TAggregate> Aggregate { get; }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            _ = info.TryAddVersionedReference(nameof(Aggregate), Aggregate);
        }
    }
}