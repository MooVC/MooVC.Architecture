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
        private static readonly Lazy<VersionedReference<TAggregate>> ActualEmpty =
            new Lazy<VersionedReference<TAggregate>>(() => new VersionedReference<TAggregate>());

        public VersionedReference(Guid id, ulong version = AggregateRoot.DefaultVersion)
            : base(id, version)
        {
            ArgumentIsAcceptable(
                id,
                nameof(id),
                value => value != Guid.Empty,
                GenericIdInvalid);
        }

        public VersionedReference(TAggregate aggregate)
            : base(aggregate)
        {
        }

        private VersionedReference()
            : base(Guid.Empty, 0)
        {
        }

        private VersionedReference(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public static VersionedReference Empty => ActualEmpty.Value;

        public override Type Type => typeof(TAggregate);
    }
}