namespace MooVC.Architecture.Ddd.DomainExceptionTests;

using System;
using System.Runtime.Serialization;
using MooVC.Architecture;

[Serializable]
public sealed class SerializableDomainException<TAggregate>
    : DomainException<TAggregate>
    where TAggregate : AggregateRoot
{
    public SerializableDomainException(Reference<TAggregate> aggregate, Message context, string message)
        : base(aggregate, context, message)
    {
    }

    public SerializableDomainException(TAggregate aggregate, Message context, string message)
        : base(aggregate, context, message)
    {
    }

    private SerializableDomainException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}