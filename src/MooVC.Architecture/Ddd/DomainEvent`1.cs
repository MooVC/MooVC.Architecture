namespace MooVC.Architecture.Ddd
{
    using System.Runtime.Serialization;

    public abstract class DomainEvent<TAggregateRoot>
        : DomainEvent
        where TAggregateRoot : AggregateRoot
    {
        protected DomainEvent(Message context, TAggregateRoot aggregate)
            : base(context, aggregate.ToReference(enforceVersion: true))
        {
        }

        protected DomainEvent(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}