namespace MooVC.Architecture.Ddd.Services;

using System;
using System.Runtime.Serialization;
using System.Threading;
using MooVC.Serialization;
using static MooVC.Architecture.Ddd.Services.Resources;
using static MooVC.Ensure;

[Serializable]
public abstract class AggregateAsyncEventArgs<TAggregate>
    : AsyncEventArgs,
      ISerializable
    where TAggregate : AggregateRoot
{
    protected AggregateAsyncEventArgs(TAggregate aggregate, CancellationToken? cancellationToken = default)
        : base(cancellationToken: cancellationToken)
    {
        Aggregate = IsNotNull(aggregate, message: AggregateEventArgsAggregateRequired);
    }

    /// <summary>
    /// Populates the specified <see cref="SerializationInfo"/> object with the data needed to serialize the current instance
    /// of the <see cref="Paging"/> class.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo"/> object that will be populated with data.</param>
    /// <param name="context">The destination (see <see cref="StreamingContext"/>) for the serialization operation.</param>
    [Obsolete(@"Slated for removal in v11 as part of Microsoft's BinaryFormatter Obsoletion Strategy.
                       (see: https://github.com/dotnet/designs/blob/main/accepted/2020/better-obsoletion/binaryformatter-obsoletion.md)")]
    protected AggregateAsyncEventArgs(SerializationInfo info, StreamingContext context)
    {
        Aggregate = info.GetValue<TAggregate>(nameof(Aggregate));
    }

    public TAggregate Aggregate { get; }

    /// <summary>
    /// Populates the specified <see cref="SerializationInfo"/> object with the data needed to serialize the current instance
    /// of the <see cref="Paging"/> class.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo"/> object that will be populated with data.</param>
    /// <param name="context">The destination (see <see cref="StreamingContext"/>) for the serialization operation.</param>
    [Obsolete(@"Slated for removal in v11 as part of Microsoft's BinaryFormatter Obsoletion Strategy.
                       (see: https://github.com/dotnet/designs/blob/main/accepted/2020/better-obsoletion/binaryformatter-obsoletion.md)")]
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue(nameof(Aggregate), Aggregate);
    }
}