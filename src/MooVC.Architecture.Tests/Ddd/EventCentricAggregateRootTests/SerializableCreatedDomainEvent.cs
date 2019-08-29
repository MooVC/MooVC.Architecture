namespace MooVC.Architecture.Ddd.EventCentricAggregateRootTests
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    internal sealed class SerializableCreatedDomainEvent
        : DomainEvent
    {
        public SerializableCreatedDomainEvent(Message context, SerializableEventCentricAggregateRoot aggregate)
            : base(context, aggregate)
        {
        }

        public SerializableCreatedDomainEvent(Message context, Reference aggregate)
            : base(context, aggregate)
        {
        }

        private SerializableCreatedDomainEvent(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}