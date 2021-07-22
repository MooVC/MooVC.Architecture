namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public abstract class DomainEvent<TAggregate>
        : DomainEvent
        where TAggregate : AggregateRoot
    {
        private readonly Lazy<Reference<TAggregate>> aggregate;

        protected DomainEvent(Message context, TAggregate aggregate)
            : base(context, aggregate.ToReference())
        {
            this.aggregate = new Lazy<Reference<TAggregate>>(aggregate.ToReference());
        }

        protected DomainEvent(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            aggregate = new Lazy<Reference<TAggregate>>(() => base.Aggregate.ToTyped<TAggregate>());
        }

        public new Reference<TAggregate> Aggregate => aggregate.Value;
    }
}