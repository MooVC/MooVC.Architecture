namespace MooVC.Architecture.Ddd;

using System;
using System.Runtime.Serialization;

[Serializable]
public abstract class DomainException<TAggregate>
    : DomainException
    where TAggregate : AggregateRoot
{
    private readonly Lazy<Reference<TAggregate>> aggregate;

    protected DomainException(Reference<TAggregate> aggregate, Message context, string message)
        : base(aggregate, context, message)
    {
        this.aggregate = new Lazy<Reference<TAggregate>>(aggregate);
    }

    protected DomainException(TAggregate aggregate, Message context, string message)
        : this(aggregate.ToReference(), context, message)
    {
    }

    protected DomainException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        aggregate = new Lazy<Reference<TAggregate>>(() => base.Aggregate.ToTyped<TAggregate>());
    }

    public new Reference<TAggregate> Aggregate => aggregate.Value;
}