namespace MooVC.Architecture.Ddd.EventCentricAggregateRootTests
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    internal sealed class SerializableFailedDomainEvent
        : DomainEvent<SerializableEventCentricAggregateRoot>
    {
        public SerializableFailedDomainEvent(Message context, SerializableEventCentricAggregateRoot aggregate)
            : base(context, aggregate)
        {
        }

        private SerializableFailedDomainEvent(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}