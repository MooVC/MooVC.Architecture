namespace MooVC.Architecture.Ddd.Services.Reconciliation;

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading;
using MooVC.Serialization;
using static MooVC.Architecture.Ddd.Services.Reconciliation.Resources;
using static MooVC.Ensure;

[Serializable]
public abstract class EventsAsyncEventArgs
    : AsyncEventArgs,
      ISerializable
{
    protected EventsAsyncEventArgs(IEnumerable<DomainEvent> events, CancellationToken? cancellationToken = default)
        : base(cancellationToken: cancellationToken)
    {
        Events = IsNotEmpty(events, message: EventsAsyncEventArgsEventsRequired);
    }

    /// <summary>
    /// Populates the specified <see cref="SerializationInfo"/> object with the data needed to serialize the current instance
    /// of the <see cref="Paging"/> class.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo"/> object that will be populated with data.</param>
    /// <param name="context">The destination (see <see cref="StreamingContext"/>) for the serialization operation.</param>
    [Obsolete(@"Slated for removal in v11 as part of Microsoft's BinaryFormatter Obsoletion Strategy.
                       (see: https://github.com/dotnet/designs/blob/main/accepted/2020/better-obsoletion/binaryformatter-obsoletion.md)")]
    protected EventsAsyncEventArgs(SerializationInfo info, StreamingContext context)
    {
        Events = info.TryGetEnumerable<DomainEvent>(nameof(Events));
    }

    public IEnumerable<DomainEvent> Events { get; }

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
        _ = info.TryAddEnumerable(nameof(Events), Events);
    }
}