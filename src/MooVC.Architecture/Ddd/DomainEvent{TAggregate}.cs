namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public abstract class DomainEvent<TAggregate>
        : DomainEvent
        where TAggregate : AggregateRoot
    {
        protected DomainEvent(Message context, TAggregate aggregate)
            : base(context, aggregate.ToVersionedReference())
        {
        }

        protected DomainEvent(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}