namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class Reference<TAggregate>
        : Reference
        where TAggregate : AggregateRoot
    {
        private static readonly Lazy<Reference<TAggregate>> ActualEmpty = 
            new Lazy<Reference<TAggregate>>(() => new Reference<TAggregate>(Guid.Empty, AggregateRoot.DefaultVersion));

        public Reference(Guid id, ulong version)
            : base(id, version)
        {
        }

        public Reference(TAggregate aggregate)
            : base(aggregate)
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