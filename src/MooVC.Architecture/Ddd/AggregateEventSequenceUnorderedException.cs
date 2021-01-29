﻿namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Collections.Generic;
    using MooVC.Collections.Generic;
    using static System.String;
    using static MooVC.Architecture.Ddd.Resources;

    [Serializable]
    public sealed class AggregateEventSequenceUnorderedException
        : ArgumentException
    {
        internal AggregateEventSequenceUnorderedException(VersionedReference aggregate, IEnumerable<DomainEvent> events)
            : base(Format(
                AggregateEventSequenceUnorderedExceptionMessage,
                aggregate.Id,
                aggregate.Version,
                aggregate.Type.Name))
        {
            Aggregate = aggregate;
            Events = events.Snapshot();
        }

        public VersionedReference Aggregate { get; }

        public IEnumerable<DomainEvent> Events { get; }
    }
}