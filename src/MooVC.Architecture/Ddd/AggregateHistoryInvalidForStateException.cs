namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Collections.Generic;
    using MooVC.Collections.Generic;
    using static System.String;
    using static Resources;

    [Serializable]
    public sealed class AggregateHistoryInvalidForStateException
        : ArgumentException
    {
        internal AggregateHistoryInvalidForStateException(
            VersionedReference aggregate,
            IEnumerable<DomainEvent> events,
            SignedVersion startingVersion)
            : base(Format(
                AggregateHistoryInvalidForStateExceptionMessage,
                aggregate.Id,
                aggregate.Version,
                aggregate.Type.Name,
                startingVersion))
        {
            Aggregate = aggregate;
            Events = events.Snapshot();
            StartingVersion = startingVersion;
        }

        public VersionedReference Aggregate { get; }

        public IEnumerable<DomainEvent> Events { get; }

        public SignedVersion StartingVersion { get; }
    }
}