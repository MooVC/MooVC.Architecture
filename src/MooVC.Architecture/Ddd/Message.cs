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
        Aggregate = IsNotEmpty(aggregate, message: Format(MessageAggregateRequired, typeof(TAggregate).Name));
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
    }
}