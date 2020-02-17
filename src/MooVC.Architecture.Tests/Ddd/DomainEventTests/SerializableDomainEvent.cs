namespace MooVC.Architecture.Ddd.DomainEventTests
{
    using System;
    using System.Runtime.Serialization;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Architecture.MessageTests;

    [Serializable]
    internal sealed class SerializableDomainEvent
        : DomainEvent
    {
        public SerializableDomainEvent(Message context = default, VersionedReference aggregate = default)
            : base(context ?? new SerializableMessage(), aggregate ?? new SerializableAggregateRoot().ToVersionedReference())
        {
        }

        private SerializableDomainEvent(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}