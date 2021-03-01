namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public abstract class DomainException<TAggregate>
        : DomainException
        where TAggregate : AggregateRoot
    {
        private readonly Lazy<Reference<TAggregate>> aggregate;

        protected DomainException(Message context, TAggregate aggregate, string message)
            : base(context, aggregate.ToReference(), message)
        {
            this.aggregate = new Lazy<Reference<TAggregate>>(aggregate.ToReference());
        }

        protected DomainException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            aggregate = new Lazy<Reference<TAggregate>>(() => base.Aggregate.ToTypedReference<TAggregate>());
        }

        public new Reference<TAggregate> Aggregate => aggregate.Value;
    }
}