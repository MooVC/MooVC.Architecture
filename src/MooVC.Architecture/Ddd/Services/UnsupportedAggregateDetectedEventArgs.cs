namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MooVC.Architecture.Ddd;
    using MooVC.Collections.Generic;
    using static MooVC.Architecture.Ddd.Ensure;
    using static MooVC.Ensure;
    using static Resources;

    public sealed class UnsupportedAggregateDetectedEventArgs
        : EventArgs
    {
        internal UnsupportedAggregateDetectedEventArgs(Reference aggregate, IEnumerable<DomainEvent> events)
        {
            ReferenceIsNotEmpty(aggregate, nameof(aggregate), UnsupportedAggregateDetectedEventArgsAggregateRequired);
            ArgumentIsAcceptable(events, nameof(events), value => value.Any(), UnsupportedAggregateDetectedEventArgsEventsRequired);

            Aggregate = aggregate;
            Events = events.Snapshot();
        }

        public Reference Aggregate { get; }

        public IEnumerable<DomainEvent> Events { get; }
    }
}
