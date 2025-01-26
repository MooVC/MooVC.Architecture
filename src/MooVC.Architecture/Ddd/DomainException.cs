namespace MooVC.Architecture.Ddd;

using System;
using MooVC.Architecture.Cqrs;

public abstract class DomainException
    : InvalidOperationException
{
    private protected DomainException(Reference aggregate, Message context, string message)
        : base(message)
    {
        Aggregate = aggregate;
        Context = context;
    }

    public Reference Aggregate { get; }

    public Message Context { get; }

    public DateTimeOffset TimeStamp { get; } = DateTimeOffset.UtcNow;
}