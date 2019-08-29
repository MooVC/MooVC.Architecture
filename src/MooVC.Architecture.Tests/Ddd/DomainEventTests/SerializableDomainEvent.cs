namespace MooVC.Architecture.Ddd.DomainEventTests
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    internal sealed class SerializableDomainEvent
        : DomainEvent
    {
        public SerializableDomainEvent(Message context, Reference aggregate) 
            : base(context, aggregate)
        {
        }

        private SerializableDomainEvent(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}