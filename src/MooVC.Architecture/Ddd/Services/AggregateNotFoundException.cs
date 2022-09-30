namespace MooVC.Architecture.Ddd.Services;

using System;
using System.Runtime.Serialization;
using MooVC.Architecture.Ddd.Serialization;
using MooVC.Serialization;
using static System.String;
using static MooVC.Architecture.Ddd.Ensure;
using static MooVC.Architecture.Ddd.Services.Resources;
using static MooVC.Ensure;

[Serializable]
public sealed class AggregateNotFoundException<TAggregate>
    : ArgumentException
    where TAggregate : AggregateRoot
{
    public AggregateNotFoundException(Reference<TAggregate> aggregate, Message context)
        : base(FormatMessage(aggregate, context))
    {
        Aggregate = aggregate;
        Context = context;
    }

    public AggregateNotFoundException(Guid aggregateId, Message context)
        : this(aggregateId.ToReference<TAggregate>(), context)
    {
    }

    private AggregateNotFoundException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        Aggregate = info.TryGetReference<TAggregate>(nameof(Aggregate));
        Context = info.GetValue<Message>(nameof(Context));
    }

    public Reference<TAggregate> Aggregate { get; }

    public Message Context { get; }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);

        _ = info.TryAddReference(nameof(Aggregate), Aggregate);
        info.AddValue(nameof(Context), Context);
    }

    private static string FormatMessage(Reference<TAggregate> aggregate, Message context)
    {
        _ = IsNotNull(context, message: AggregateNotFoundExceptionContextRequired);
        _ = ReferenceIsNotEmpty(aggregate, nameof(aggregate), AggregateNotFoundExceptionAggregateRequired);

        return Format(AggregateNotFoundExceptionMessage, aggregate.Id, aggregate.Type.Name);
    }
}