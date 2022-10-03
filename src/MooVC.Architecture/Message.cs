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

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);

        SerializeCausationId(info, context);
        SerializeCorrelationId(info, context);

        info.AddValue(nameof(TimeStamp), TimeStamp);
    }

    public override string ToString()
    {
        return $"{GetType().FullName} [{Id:P}, {CorrelationId:P}]";
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