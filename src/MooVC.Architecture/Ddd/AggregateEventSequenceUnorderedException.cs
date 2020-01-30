namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Collections.Generic;
    using MooVC.Collections.Generic;
    using static System.String;
    using static Resources;

    [Serializable]
    public sealed class AggregateEventSequenceUnorderedException
        : ArgumentException
    {
        internal AggregateEventSequenceUnorderedException(VersionedReference aggregate, IEnumerable<DomainEvent> events)
            : base(Format(
                AggregateEventSequenceUnorderedExceptionMessage,
                aggregate.Id,
                aggregate.Type.Name,
                aggregate.Version))
        {
            Aggregate = aggregate;
            Events = events.Snapshot();
        }

        public VersionedReference Aggregate { get; }

        public IEnumerable<DomainEvent> Events { get; }
    }
}