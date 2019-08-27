namespace MooVC.Architecture.Ddd.DomainEventTests
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    internal sealed class SerializableDomainEvent
        : DomainEvent
    {
        public SerializableDomainEvent(Message context, IReference aggregate, ulong version) 
            : base(context, aggregate, version)
        {
        }

        private SerializableDomainEvent(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}