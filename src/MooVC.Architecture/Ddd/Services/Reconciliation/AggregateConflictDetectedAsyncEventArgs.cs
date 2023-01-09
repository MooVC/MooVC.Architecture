namespace MooVC.Architecture.Ddd.Services.Reconciliation;

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading;
using MooVC.Architecture.Ddd;
using MooVC.Architecture.Ddd.Serialization;
using MooVC.Serialization;
using static MooVC.Architecture.Ddd.Ensure;
using static MooVC.Architecture.Ddd.Services.Reconciliation.Resources;
using static MooVC.Ensure;

[Serializable]
public sealed class AggregateConflictDetectedAsyncEventArgs
    : AsyncEventArgs,
      ISerializable
{
    internal AggregateConflictDetectedAsyncEventArgs(
        Reference aggregate,
        IEnumerable<DomainEvent> events,
        SignedVersion next,
        SignedVersion previous,
        CancellationToken? cancellationToken = default)
        : base(cancellationToken: cancellationToken)
    {
        Aggregate = IsNotEmpty(aggregate, message: AggregateConflictDetectedEventArgsAggregateRequired);
        Events = IsNotEmpty(events, message: AggregateConflictDetectedEventArgsEventsRequired);
        Next = IsNotNull(next, message: AggregateConflictDetectedEventArgsNextRequired);
        Previous = IsNotNull(previous, message: AggregateConflictDetectedEventArgsPreviousRequired);
    }

    /// <summary>
    /// Populates the specified <see cref="SerializationInfo"/> object with the data needed to serialize the current instance
    /// of the <see cref="Paging"/> class.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo"/> object that will be populated with data.</param>
    /// <param name="context">The destination (see <see cref="StreamingContext"/>) for the serialization operation.</param>
    [Obsolete(@"Slated for removal in v11 as part of Microsoft's BinaryFormatter Obsoletion Strategy.
                       (see: https://github.com/dotnet/designs/blob/main/accepted/2020/better-obsoletion/binaryformatter-obsoletion.md)")]
    private AggregateConflictDetectedAsyncEventArgs(SerializationInfo info, StreamingContext context)
    {
        Aggregate = info.TryGetReference(nameof(Aggregate));
        Events = info.TryGetEnumerable<DomainEvent>(nameof(Events));
        Next = info.GetValue<SignedVersion>(nameof(Next));
        Previous = info.GetValue<SignedVersion>(nameof(Previous));
    }

    public Reference Aggregate { get; }

    public IEnumerable<DomainEvent> Events { get; }

    public SignedVersion Next { get; }

    public SignedVersion Previous { get; }

    /// <summary>
    /// Populates the specified <see cref="SerializationInfo"/> object with the data needed to serialize the current instance
    /// of the <see cref="Paging"/> class.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo"/> object that will be populated with data.</param>
    /// <param name="context">The destination (see <see cref="StreamingContext"/>) for the serialization operation.</param>
    [Obsolete(@"Slated for removal in v11 as part of Microsoft's BinaryFormatter Obsoletion Strategy.
                       (see: https://github.com/dotnet/designs/blob/main/accepted/2020/better-obsoletion/binaryformatter-obsoletion.md)")]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        _ = info.TryAddValue(nameof(Aggregate), Aggregate);
        _ = info.TryAddEnumerable(nameof(Events), Events);
        info.AddValue(nameof(Next), Next);
        info.AddValue(nameof(Previous), Previous);
    }
}