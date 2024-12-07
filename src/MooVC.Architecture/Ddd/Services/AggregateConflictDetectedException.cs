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
public abstract class AggregateConflictDetectedException
    : ArgumentException
{
    private protected AggregateConflictDetectedException(Reference aggregate, Sequence received)
        : base(FormatMessage(aggregate, received))
    {
        Aggregate = aggregate;
        Persisted = Sequence.Empty;
        Received = received;
    }

    private protected AggregateConflictDetectedException(Reference aggregate, Sequence persisted, Sequence received)
        : base(FormatMessage(aggregate, received, persisted: persisted, persistedRequired: true))
    {
        Aggregate = aggregate;
        Persisted = persisted;
        Received = received;
    }

    private protected AggregateConflictDetectedException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        Aggregate = info.TryGetReference(nameof(Aggregate));
        Persisted = info.TryGetValue(nameof(Persisted), defaultValue: Sequence.Empty);
        Received = info.TryGetValue(nameof(Received), defaultValue: Sequence.Empty);
    }

    public Reference Aggregate { get; }

    public Sequence Persisted { get; }

    public Sequence Received { get; }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);

        info.AddValue(nameof(Aggregate), Aggregate);
        _ = info.TryAddValue(nameof(Persisted), Persisted, defaultValue: Sequence.Empty);
        _ = info.TryAddValue(nameof(Received), Received, defaultValue: Sequence.Empty);
    }

    private static string FormatMessage(
        Reference aggregate,
        Sequence received,
        Sequence? persisted = default,
        bool persistedRequired = false)
    {
        _ = IsNotEmpty(aggregate, message: AggregateConflictDetectedExceptionAggregateRequired);
        _ = IsNotNull(received, message: AggregateConflictDetectedExceptionReceivedRequired);

        if (persistedRequired)
        {
            _ = IsNotNull(persisted, message: AggregateConflictDetectedExceptionPersistedRequired);

            return Format(AggregateConflictDetectedExceptionExistingEntryMessage, aggregate.Id, aggregate.Type.Name, received, persisted);
        }

        return Format(AggregateConflictDetectedExceptionNoExistingEntryMessage, aggregate.Id, aggregate.Type.Name, received);
    }
}