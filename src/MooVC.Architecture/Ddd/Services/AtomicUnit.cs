namespace MooVC.Architecture.Ddd.Services;

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MooVC.Architecture.Ddd;

[Serializable]
public sealed class AtomicUnit
    : AtomicUnit<Guid>
{
    public AtomicUnit(DomainEvent @event)
        : base(@event, Guid.NewGuid())
    {
    }

    public AtomicUnit(IEnumerable<DomainEvent> events)
        : base(events, Guid.NewGuid())
    {
    }

    private AtomicUnit(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}