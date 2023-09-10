namespace MooVC.Architecture.Cqrs;

using Ardalis.GuardClauses;

public sealed record Trace
{
    public Trace(Guid causationId)
        : this(causationId, Guid.NewGuid(), DateTimeOffset.UtcNow)
    {
    }

    public Trace(Guid causationId, Guid correlationId, DateTimeOffset timestamp)
    {
        CausationId = Guard.Against.NullOrEmpty(causationId, message: CausationIdRequired);
        CorrelationId = Guard.Against.NullOrEmpty(correlationId, message: CausationIdRequired);
        Timestamp = Guard.Against.NullOrEmpty(timestamp, message: TimestampRequired);
    }

    public Guid CausationId { get; }

    public Guid CorrelationId { get; }

    public DateTimeOffset Timestamp { get; }

    public static implicit operator Trace(Message message)
    {
        _ = Guard.Against.Null(message, message: MessageRequired);

        return new Trace(message.Id, message.Trace.CorrelationId, DateTimeOffset.UtcNow);
    }
}