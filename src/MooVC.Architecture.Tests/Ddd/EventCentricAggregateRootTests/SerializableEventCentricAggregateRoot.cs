namespace MooVC.Architecture.Ddd.EventCentricAggregateRootTests
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    internal sealed class SerializableEventCentricAggregateRoot
        : EventCentricAggregateRoot
    {
        public SerializableEventCentricAggregateRoot()
            : base(Guid.NewGuid())
        {
        }

        public SerializableEventCentricAggregateRoot(Message context)
            : this(Guid.NewGuid())
        {
            ApplyChange(() => new SerializableCreatedDomainEvent(context, this));
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

        public void Set(SetRequest request)
        {
            ApplyChange(() => new SerializableSetDomainEvent(request.Context, this, request.Value));
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