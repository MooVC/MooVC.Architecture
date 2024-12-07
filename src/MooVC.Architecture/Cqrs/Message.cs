namespace MooVC.Architecture.Cqrs;

using System;
using Ardalis.GuardClauses;
using MooVC.Threading;
using static MooVC.Architecture.Cqrs.Message_Resources;

/// <summary>
/// Represents an abstract base record for all messages within the CQRS (Command Query Responsibility Segregation) architectural style.
/// </summary>
public abstract record Message
    : ICoordinatable
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
        Id = Guard.Against.NullOrEmpty(id, nameof(id), IdRequired);
        Trace = Guard.Against.Null(trace, nameof(trace), TraceRequired);
    }

    /// <summary>
    /// Gets the unique identifier of the message.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Gets the trace information associated with the message.
    /// </summary>
    public Trace Trace { get; }

    /// <summary>
    /// Returns a string that represents the current <see cref="Message"/>.
    /// </summary>
    /// <returns>A string representation of the <see cref="Message"/>.</returns>
    public override string ToString()
    {
        return $"{GetType().FullName} [{Id:D}]";
    }

    /// <summary>
    /// Retrieves a key for this instance, facilitating the use of <see cref="ICoordinator{Message}"/>.
    /// </summary>
    /// <returns>A unique identifier for the message.</returns>
    string ICoordinatable.GetKey()
    {
        return ToString();
    }
}