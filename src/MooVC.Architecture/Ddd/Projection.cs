namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Runtime.Serialization;
    using MooVC.Architecture.Serialization;
    using static MooVC.Architecture.Ddd.Ensure;
    using static MooVC.Architecture.Ddd.Resources;

    [Serializable]
    public abstract class Projection<TAggregate>
        : ISerializable
        where TAggregate : AggregateRoot
    {
        protected Projection(TAggregate aggregate)
            : this(CreateReference(aggregate))
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

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            _ = info.TryAddVersionedReference(nameof(Aggregate), Aggregate);
        }

        private static VersionedReference<TAggregate> CreateReference(TAggregate aggregate)
        {
            return aggregate?.ToVersionedReference() ?? VersionedReference<TAggregate>.Empty;
        }
    }
}