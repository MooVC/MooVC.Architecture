namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MooVC.Architecture.Ddd;
    using static MooVC.Architecture.Ddd.Ensure;
    using static MooVC.Ensure;
    using static Resources;

    public sealed class AggregateConflictDetectedEventArgs
        : EventArgs
    {
        internal AggregateConflictDetectedEventArgs(
            Reference aggregate,
            IEnumerable<DomainEvent> events,
            SignedVersion next,
            SignedVersion previous)
        {
            ReferenceIsNotEmpty(aggregate, nameof(aggregate), AggregateConflictDetectedEventArgsAggregateRequired);
            ArgumentIsAcceptable(events, nameof(events), value => value.Any(), AggregateConflictDetectedEventArgsEventsRequired);
            ArgumentNotNull(next, nameof(next), AggregateConflictDetectedEventArgsNextRequired);
            ArgumentNotNull(previous, nameof(previous), AggregateConflictDetectedEventArgsPreviousRequired);

            Aggregate = aggregate;
            Events = events;
            Next = next;
            Previous = previous;
        }

        public Reference Aggregate { get; }

        public IEnumerable<DomainEvent> Events { get; }

        public SignedVersion Next { get; }

        public SignedVersion Previous { get; }
    }
}