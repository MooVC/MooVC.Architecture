﻿namespace MooVC.Architecture.Ddd.Services.Reconciliation;

using System.Collections.Generic;

public interface ISequencedEvents
{
    Reference Aggregate { get; }

    public IEnumerable<DomainEvent> Events { get; }

    ulong Sequence { get; }
}