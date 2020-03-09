namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Runtime.Serialization;
    using static MooVC.Ensure;
    using static Resources;

    [Serializable]
    public sealed class VersionedReference<TAggregate>
        : VersionedReference
        where TAggregate : AggregateRoot
    {
        private static readonly Lazy<VersionedReference<TAggregate>> empty =
            new Lazy<VersionedReference<TAggregate>>(() => new VersionedReference<TAggregate>());

        private readonly Lazy<Reference<TAggregate>> actualReference;

        public VersionedReference(Guid id, SignedVersion version)
            : base(id, version)
        {
            ArgumentIsAcceptable(
                id,
                nameof(id),
                value => value != Guid.Empty,
                GenericIdInvalid);

            actualReference = new Lazy<Reference<TAggregate>>(() => this.ToReference());
        }

        public VersionedReference(TAggregate aggregate)
            : base(aggregate)
        {
            actualReference = new Lazy<Reference<TAggregate>>(() => this.ToReference());
        }

        private VersionedReference()
            : base(Guid.Empty, SignedVersion.Empty)
        {
            actualReference = new Lazy<Reference<TAggregate>>(() => Reference<TAggregate>.Empty);
        }

        private VersionedReference(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            actualReference = new Lazy<Reference<TAggregate>>(() => this.ToReference());
        }

        public static VersionedReference<TAggregate> Empty => empty.Value;

        public Reference<TAggregate> Reference => actualReference.Value;

        public override Type Type => typeof(TAggregate);
    }
}