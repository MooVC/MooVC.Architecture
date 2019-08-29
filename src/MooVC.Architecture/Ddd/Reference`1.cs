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
        private static readonly Lazy<Reference<TAggregate>> ActualEmpty = 
            new Lazy<Reference<TAggregate>>(() => new Reference<TAggregate>());
        
        public Reference(Guid id, ulong version = AggregateRoot.DefaultVersion)
            : base(id, version)
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
            : base(Guid.Empty, 0)
        {
        }

        private Reference(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public static Reference Empty => ActualEmpty.Value;

        public override Type Type => typeof(TAggregate);
    }
}