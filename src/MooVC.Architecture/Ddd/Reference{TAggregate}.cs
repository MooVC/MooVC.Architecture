namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Runtime.Serialization;
    using static MooVC.Ensure;
    using static Resources;

    [Serializable]
    public sealed class Reference<TAggregate>
        : Reference
        where TAggregate : AggregateRoot
    {
        private static readonly Lazy<Reference<TAggregate>> empty =
            new Lazy<Reference<TAggregate>>(() => new Reference<TAggregate>());

        public Reference(Guid id)
            : base(id, typeof(TAggregate))
        {
            ArgumentIsAcceptable(
                id,
                nameof(id),
                value => value != Guid.Empty,
                GenericIdInvalid);
        }

        public Reference(TAggregate aggregate)
            : this(aggregate.Id)
        {
        }

        private Reference()
            : base(Guid.Empty, typeof(TAggregate))
        {
        }

        private Reference(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public static Reference<TAggregate> Empty => empty.Value;

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