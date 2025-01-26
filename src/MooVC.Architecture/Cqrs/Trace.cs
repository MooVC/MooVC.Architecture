namespace MooVC.Architecture.Cqrs;

using Ardalis.GuardClauses;
using static MooVC.Architecture.Cqrs.Trace_Resources;

/// <summary>
/// Facilitates distributed tracing by providing causation and correlation identifiers, along with a timestamp.
/// </summary>
public sealed record Trace
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Trace"/> record for a given causation identifier, automatically generating a unique
    /// correlation identifier and setting the timestamp to the current UTC time.
    /// </summary>
    /// <param name="causationId">
    /// The causation identifier that initiated the trace.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="causationId"/> is empty.
    /// </exception>
    public Trace(Guid causationId)
        : this(causationId, Guid.NewGuid(), DateTimeOffset.UtcNow)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Trace"/> record with specified causation and correlation identifiers, and a timestamp.
    /// </summary>
    /// <param name="causationId">
    /// The causation identifier that initiated the trace.
    /// </param>
    /// <param name="correlationId">
    /// A unique identifier to correlate related operations.
    /// </param>
    /// <param name="timestamp">
    /// The timestamp marking the creation of the trace.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="causationId"/> or <paramref name="correlationId"/> is empty.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="timestamp"/> is the default value, indicating an invalid timestamp.
    /// </exception>
    public Trace(Guid causationId, Guid correlationId, DateTimeOffset timestamp)
    {
        CausationId = Guard.Against.NullOrEmpty(causationId, message: CausationIdRequired);
        CorrelationId = Guard.Against.NullOrEmpty(correlationId, message: CorrelationIdRequired);
        Timestamp = Guard.Against.Default(timestamp, message: TimestampRequired);
    }

    /// <summary>
    /// Gets the causation identifier, representing the unique identifier for the action that initiated the series of events.
    /// </summary>
    /// <value>
    /// The causation identifier, representing the unique identifier for the action that initiated the series of events.
    /// </value>
    public Guid CausationId { get; }

    /// <summary>
    /// Gets the correlation identifier, used to correlate this action with other related actions.
    /// </summary>
    /// <value>
    /// The correlation identifier, used to correlate this action with other related actions.
    /// </value>
    public Guid CorrelationId { get; }

    /// <summary>
    /// Gets the timestamp marking when the trace was created.
    /// </summary>
    /// <value>
    /// The timestamp marking when the trace was created.
    /// </value>
    public DateTimeOffset Timestamp { get; }

    /// <summary>
    /// Provides an implicit conversion from a <see cref="Message"/> to a <see cref="Trace"/>, allowing a message to be directly used where
    /// a Trace is expected.
    /// This conversion creates a new <see cref="Trace"/> instance using the message's ID as the causation ID, the message's trace correlation ID,
    /// and the current UTC timestamp.
    /// </summary>
    /// <param name="message">
    /// The message to convert into a trace.
    /// </param>
    /// <returns>
    /// A new <see cref="Trace"/> instance derived from the message.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="message"/> is <c>null</c>.
    /// </exception>
    public static implicit operator Trace(Message message)
    {
        _ = Guard.Against.Null(message, message: MessageRequired);

        return new Trace(message.Id, message.Trace.CorrelationId, DateTimeOffset.UtcNow);
    }

    /// <summary>
    /// Returns a string that represents the current <see cref="Trace"/>.
    /// </summary>
    /// <returns>
    /// A string representation of the <see cref="Trace"/>.
    /// </returns>
    public override string ToString()
    {
        return $"{GetType()} [{nameof(CausationId)}: {CausationId:D}; {nameof(CorrelationId)}: {CorrelationId:D}; {nameof(Timestamp)}: {Timestamp:u}]";
    }
}