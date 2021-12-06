namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class SerializableEventCentricAggregateRoot
        : EventCentricAggregateRoot
    {
        public SerializableEventCentricAggregateRoot()
            : base(Guid.NewGuid())
        {
        }

        public SerializableEventCentricAggregateRoot(Message context)
            : this(Guid.NewGuid())
        {
            ApplyChange(() => new SerializableCreatedDomainEvent(context, this), Handle);
        }

        public SerializableEventCentricAggregateRoot(Guid id)
            : base(id)
        {
        }

        private SerializableEventCentricAggregateRoot(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public Guid Value { get; private set; }

        public void Fail(FailRequest request)
        {
            ApplyChange(() => new SerializableFailedDomainEvent(request.Context, this), Handle);
        }

        public void Set(SetRequest request)
        {
            ApplyChange(() => new SerializableSetDomainEvent(request.Context, this, request.Value), Handle);
        }

        public void TriggerMarkChangesAsUncommitted()
        {
            MarkChangesAsUncommitted();
        }

        public void TriggerRollbackUncommittedChanges()
        {
            RollbackUncommittedChanges();
        }

        private void Handle(SerializableFailedDomainEvent @event)
        {
            throw new InvalidOperationException();
        }

        private void Handle(SerializableCreatedDomainEvent @event)
        {
            Value = @event.Aggregate.Id;
        }

        private void Handle(SerializableSetDomainEvent @event)
        {
            Value = @event.Value;
        }
    }
}