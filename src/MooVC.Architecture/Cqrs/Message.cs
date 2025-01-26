namespace MooVC.Architecture.Cqrs;

using Ardalis.GuardClauses;
using static MooVC.Architecture.Cqrs.Message_Resources;

/// <summary>
/// Represents an abstract base record for all messages within the CQRS (Command Query Responsibility Segregation) architectural style.
/// </summary>
public abstract record Message
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Message"/> record with a unique identifier and a new trace record.
    /// </summary>
    protected Message()
    {
        Id = Guid.NewGuid();
        Trace = new Trace(Id);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Message"/> record with a specified identifier and trace information.
    /// </summary>
    /// <param name="id">The unique identifier of the message.</param>
    /// <param name="trace">The trace information associated with the message.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="trace"/> is <c>null</c>.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="id"/> is empty.</exception>
    protected Message(Guid id, Trace trace)
    {
        Id = Guard.Against.NullOrEmpty(id, message: IdRequired);
        Trace = Guard.Against.Null(trace, message: TraceRequired);
    }

    /// <summary>
    /// Gets the unique identifier of the message.
    /// </summary>
    /// <value>
    /// The unique identifier of the message.
    /// </value>
    public Guid Id { get; }

    /// <summary>
    /// Gets the trace information associated with the message.
    /// </summary>
    /// <value>
    /// The trace information associated with the message.
    /// </value>
    public Trace Trace { get; }

    /// <summary>
    /// Returns a string that represents the current <see cref="Message"/>.
    /// </summary>
    /// <returns>A string representation of the <see cref="Message"/>.</returns>
    public override string ToString()
    {
        return $"{GetType()} [{Id:D}]";
    }
}