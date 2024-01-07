namespace MooVC.Architecture.Cqrs;

using System;
using Ardalis.GuardClauses;
using static MooVC.Architecture.Cqrs.Message_Resources;

public abstract record Message
{
    protected Message()
        : this(Guid.NewGuid())
    {
    }

    protected Message(Guid id, Trace trace)
    {
        Id = Guard.Against.NullOrEmpty(id, message: IdRequired);
        Trace = Guard.Against.Null(trace, message: TraceRequired);
    }

    private Message(Guid id)
        : this(id, new Trace(id))
    {
    }

    public Guid Id { get; }

    public Trace Trace { get; }
}