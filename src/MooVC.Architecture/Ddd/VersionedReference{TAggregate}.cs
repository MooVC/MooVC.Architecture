namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Runtime.Serialization;
    using static MooVC.Architecture.Ddd.Resources;
    using static MooVC.Ensure;

    [Serializable]
    public sealed class VersionedReference<TAggregate>
        : VersionedReference
        where TAggregate : AggregateRoot
    {
        private static readonly Lazy<VersionedReference<TAggregate>> empty =
            new Lazy<VersionedReference<TAggregate>>(() => new VersionedReference<TAggregate>());

        private readonly Lazy<Reference<TAggregate>> reference;

        public VersionedReference(Guid id, SignedVersion version)
            : base(id, typeof(TAggregate), version)
        {
            ArgumentIsAcceptable(
                id,
                nameof(id),
                value => value != Guid.Empty,
                VersionedReferenceIdRequired);

            reference = new Lazy<Reference<TAggregate>>(() => this.ToReference());
        }

        public VersionedReference(TAggregate aggregate)
            : this(aggregate.Id, aggregate.Version)
        {
        }

        private VersionedReference()
            : base(Guid.Empty, typeof(TAggregate), SignedVersion.Empty)
        {
            reference = new Lazy<Reference<TAggregate>>(() => Reference<TAggregate>.Empty);
        }

        private VersionedReference(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            reference = new Lazy<Reference<TAggregate>>(() => this.ToReference());
        }

        public static VersionedReference<TAggregate> Empty => empty.Value;

        public Reference<TAggregate> Reference => reference.Value;

        protected override Type DeserializeType(SerializationInfo info, StreamingContext context)
        {
            return typeof(TAggregate);
        }

        protected override void SerializeType(SerializationInfo info, StreamingContext context)
        {
            // Do nothing - The Type is already known
        }
    }
}