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
public sealed class AggregateVersionNotFoundException<TAggregate>
    : ArgumentException
    where TAggregate : AggregateRoot
{
    public AggregateVersionNotFoundException(Message context, Reference<TAggregate> aggregate)
        : base(FormatMessage(context, aggregate))
    {
        Aggregate = aggregate;
        Context = context;
    }

    public AggregateVersionNotFoundException(Message context, Guid aggregateId, SignedVersion? version = default)
        : this(context, new Reference<TAggregate>(aggregateId, version: version))
    {
    }

    private AggregateVersionNotFoundException(SerializationInfo info, StreamingContext context)
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

    private static string FormatMessage(Message context, Reference<TAggregate> aggregate)
    {
        _ = ReferenceIsNotEmpty(aggregate, nameof(aggregate), AggregateVersionNotFoundExceptionAggregateRequired);
        _ = ArgumentNotNull(context, nameof(context), AggregateVersionNotFoundExceptionContextRequired);

        return Format(AggregateVersionNotFoundExceptionMessage, aggregate.Id, aggregate.Type.Name, aggregate.Version);
    }
}