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
            : base(id)
        {
            ArgumentIsAcceptable(
                id,
                nameof(id),
                value => value != Guid.Empty,
                GenericIdInvalid);
        }

        public Reference(TAggregate aggregate)
            : base(aggregate)
        {
        }

        private Reference()
            : base(Guid.Empty)
        {
        }

        private Reference(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public static Reference<TAggregate> Empty => empty.Value;

        public override Type Type => typeof(TAggregate);
    }
}