namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Runtime.Serialization;
    using static MooVC.Architecture.Ddd.Resources;
    using static MooVC.Ensure;

    [Serializable]
    public sealed class Reference<TAggregate>
        : Reference
        where TAggregate : AggregateRoot
    {
        private static readonly Lazy<Reference<TAggregate>> empty =
            new Lazy<Reference<TAggregate>>(() => new Reference<TAggregate>());

        public Reference(Guid id, SignedVersion? version = default)
            : base(id, typeof(TAggregate), version: version)
        {
            ArgumentIsAcceptable(
                id,
                nameof(id),
                value => value != Guid.Empty,
                ReferenceIdRequired);
        }

        public Reference(TAggregate aggregate)
            : base(aggregate)
        {
        }

        private Reference()
            : base(Guid.Empty, typeof(TAggregate), SignedVersion.Empty)
        {
        }

        private Reference(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public static Reference<TAggregate> Empty => empty.Value;

        public override Type Type => typeof(TAggregate);

        protected override Type DeserializeType(SerializationInfo info, StreamingContext context)
        {
            return typeof(TAggregate);
        }

        protected override void SerializeType(SerializationInfo info, StreamingContext context)
        {
        }
    }
}