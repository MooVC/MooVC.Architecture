namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class AggregateConflictDetectedException<TAggregate>
        : AggregateConflictDetectedException
        where TAggregate : AggregateRoot
    {
        public AggregateConflictDetectedException(Reference<TAggregate> aggregate, SignedVersion received)
            : base(aggregate, received)
        {
        }

        public AggregateConflictDetectedException(Guid aggregateId, SignedVersion received)
            : this(new Reference<TAggregate>(aggregateId), received)
        {
        }

        public AggregateConflictDetectedException(Reference<TAggregate> aggregate, SignedVersion persisted, SignedVersion received)
            : base(aggregate, persisted, received)
        {
        }

        public AggregateConflictDetectedException(Guid aggregateId, SignedVersion persisted, SignedVersion received)
            : this(new Reference<TAggregate>(aggregateId), persisted, received)
        {
        }

        private AggregateConflictDetectedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public new Reference<TAggregate> Aggregate => base.Aggregate.ToTypedReference<TAggregate>();
    }
}