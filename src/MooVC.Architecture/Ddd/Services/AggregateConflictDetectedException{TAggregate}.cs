namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class AggregateConflictDetectedException<TAggregate>
        : AggregateConflictDetectedException
        where TAggregate : AggregateRoot
    {
        public AggregateConflictDetectedException(Reference<TAggregate> aggregate, SignedVersion receivedVersion)
            : base(aggregate, receivedVersion)
        {
        }

        public AggregateConflictDetectedException(Guid aggregateId, SignedVersion receivedVersion)
            : this(new Reference<TAggregate>(aggregateId), receivedVersion)
        {
        }

        public AggregateConflictDetectedException(Reference aggregate, SignedVersion persistedVersion, SignedVersion receivedVersion)
            : base(aggregate, persistedVersion, receivedVersion)
        {
        }

        public AggregateConflictDetectedException(Guid aggregateId, SignedVersion persistedVersion, SignedVersion receivedVersion)
            : this(new Reference<TAggregate>(aggregateId), persistedVersion, receivedVersion)
        {
        }

        private AggregateConflictDetectedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public new Reference<TAggregate> Aggregate => base.Aggregate.ToTypedReference<TAggregate>();
    }
}