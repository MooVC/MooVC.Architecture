namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MooVC.Architecture.Ddd;
    using static MooVC.Architecture.Ddd.Ensure;
    using static MooVC.Ensure;
    using static Resources;

    public sealed class AggregateReconciledEventArgs
        : EventArgs
    {
        internal AggregateReconciledEventArgs(
            Reference aggregate,
            IEnumerable<DomainEvent> events)
        {
            ReferenceIsNotEmpty(aggregate, nameof(aggregate), AggregateReconciledEventArgsAggregateRequired);
            ArgumentIsAcceptable(events, nameof(events), value => value.Any(), AggregateReconciledEventArgsEventsRequired);

            Aggregate = aggregate;
            Events = events;
        }

        public Reference Aggregate { get; }

        public IEnumerable<DomainEvent> Events { get; }
    }
}