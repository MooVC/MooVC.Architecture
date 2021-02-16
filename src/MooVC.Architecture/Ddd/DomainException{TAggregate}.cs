namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public abstract class DomainException<TAggregate>
        : DomainException
        where TAggregate : AggregateRoot
    {
        protected DomainException(Message context, TAggregate aggregate, string message)
            : base(context, aggregate.ToReference(), message)
        {
        }

        protected DomainException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}