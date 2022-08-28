namespace MooVC.Architecture.Ddd;

using System.Runtime.Serialization;
using MooVC.Architecture.Ddd.Serialization;
using static System.String;
using static MooVC.Architecture.Ddd.Ensure;
using static MooVC.Architecture.Ddd.Resources;

public abstract class Message<TAggregate>
    : Message
    where TAggregate : AggregateRoot
{
    protected Message(Reference<TAggregate> aggregate, Message? context = default)
        : base(context: context)
    {
        Aggregate = ReferenceIsNotEmpty(aggregate, nameof(aggregate), Format(MessageAggregateRequired, typeof(TAggregate).Name));
    }

    protected Message(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        Aggregate = info.TryGetReference<TAggregate>(nameof(Aggregate));
    }

    public Reference<TAggregate> Aggregate { get; }

    public static implicit operator Reference<TAggregate>(Message<TAggregate>? message)
    {
        if (message is { })
        {
            return message.Aggregate;
        }

        return Reference<TAggregate>.Empty;
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);

        _ = info.TryAddReference(nameof(Aggregate), Aggregate);
    }
}