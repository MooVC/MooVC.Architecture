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
    public AggregateVersionNotFoundException(Reference<TAggregate> aggregate, Message context)
        : base(FormatMessage(aggregate, context))
    {
        Aggregate = aggregate;
        Context = context;
    }

    public AggregateVersionNotFoundException(Guid aggregateId, Message context, SignedVersion? version = default)
        : this(new Reference<TAggregate>(aggregateId, version: version), context)
    {
    }

    /// <summary>
    /// Populates the specified <see cref="SerializationInfo"/> object with the data needed to serialize the current instance
    /// of the <see cref="Paging"/> class.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo"/> object that will be populated with data.</param>
    /// <param name="context">The destination (see <see cref="StreamingContext"/>) for the serialization operation.</param>
    [Obsolete(@"Slated for removal in v11 as part of Microsoft's BinaryFormatter Obsoletion Strategy.
                       (see: https://github.com/dotnet/designs/blob/main/accepted/2020/better-obsoletion/binaryformatter-obsoletion.md)")]
    private AggregateVersionNotFoundException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        Aggregate = info.TryGetReference<TAggregate>(nameof(Aggregate));
        Context = info.GetValue<Message>(nameof(Context));
    }

    public Reference<TAggregate> Aggregate { get; }

    public Message Context { get; }

    /// <summary>
    /// Populates the specified <see cref="SerializationInfo"/> object with the data needed to serialize the current instance
    /// of the <see cref="Paging"/> class.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo"/> object that will be populated with data.</param>
    /// <param name="context">The destination (see <see cref="StreamingContext"/>) for the serialization operation.</param>
    [Obsolete(@"Slated for removal in v11 as part of Microsoft's BinaryFormatter Obsoletion Strategy.
                       (see: https://github.com/dotnet/designs/blob/main/accepted/2020/better-obsoletion/binaryformatter-obsoletion.md)")]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);

        _ = info.TryAddReference(nameof(Aggregate), Aggregate);
        info.AddValue(nameof(Context), Context);
    }

    private static string FormatMessage(Reference<TAggregate> aggregate, Message context)
    {
        _ = IsNotEmpty(aggregate, message: AggregateVersionNotFoundExceptionAggregateRequired);
        _ = IsNotNull(context, message: AggregateVersionNotFoundExceptionContextRequired);

        return Format(AggregateVersionNotFoundExceptionMessage, aggregate.Id, aggregate.Type.Name, aggregate.Version);
    }
}