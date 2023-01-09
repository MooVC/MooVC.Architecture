namespace MooVC.Architecture;

using System;
using System.Runtime.Serialization;
using MooVC.Serialization;
using MooVC.Threading;

[Serializable]
public abstract class Message
    : Entity<Guid>,
      ICoordinatable<Guid>
{
    protected Message(Message? context = default)
        : base(Guid.NewGuid())
    {
        if (context is { })
        {
            CausationId = context.Id;
            CorrelationId = context.CorrelationId;
        }
    }

    /// <summary>
    /// Populates the specified <see cref="SerializationInfo"/> object with the data needed to serialize the current instance
    /// of the <see cref="Paging"/> class.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo"/> object that will be populated with data.</param>
    /// <param name="context">The destination (see <see cref="StreamingContext"/>) for the serialization operation.</param>
    [Obsolete(@"Slated for removal in v11 as part of Microsoft's BinaryFormatter Obsoletion Strategy.
                       (see: https://github.com/dotnet/designs/blob/main/accepted/2020/better-obsoletion/binaryformatter-obsoletion.md)")]
    protected Message(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        CausationId = DeserializeCausationId(info, context);
        CorrelationId = DeserializeCorrelationId(info, context);
        TimeStamp = info.GetValue<DateTimeOffset>(nameof(TimeStamp));
    }

    public virtual Guid CausationId { get; } = Guid.Empty;

    public virtual Guid CorrelationId { get; } = Guid.NewGuid();

    public DateTimeOffset TimeStamp { get; } = DateTimeOffset.UtcNow;

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

        SerializeCausationId(info, context);
        SerializeCorrelationId(info, context);

        info.AddValue(nameof(TimeStamp), TimeStamp);
    }

    public override string ToString()
    {
        return $"{GetType().FullName} [{Id:D}, {CorrelationId:D}]";
    }

    Guid ICoordinatable<Guid>.GetKey()
    {
        return CorrelationId;
    }

    protected virtual Guid DeserializeCausationId(SerializationInfo info, StreamingContext context)
    {
        return info.TryGetValue<Guid>(nameof(CausationId));
    }

    protected virtual Guid DeserializeCorrelationId(SerializationInfo info, StreamingContext context)
    {
        return info.GetValue<Guid>(nameof(CorrelationId));
    }

    protected virtual void SerializeCausationId(SerializationInfo info, StreamingContext context)
    {
        _ = info.TryAddValue(nameof(CausationId), CausationId);
    }

    protected virtual void SerializeCorrelationId(SerializationInfo info, StreamingContext context)
    {
        info.AddValue(nameof(CorrelationId), CorrelationId);
    }
}