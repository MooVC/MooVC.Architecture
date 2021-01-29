namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using MooVC.Architecture.Ddd;
    using MooVC.Serialization;
    using static MooVC.Architecture.Ddd.Ensure;
    using static MooVC.Architecture.Ddd.Services.Reconciliation.Resources;
    using static MooVC.Ensure;

    [Serializable]
    public sealed class AggregateConflictDetectedEventArgs
        : EventArgs,
          ISerializable
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

        private AggregateConflictDetectedEventArgs(SerializationInfo info, StreamingContext context)
        {
            Aggregate = info.TryGetValue<Reference>(nameof(Aggregate));
            Events = info.TryGetEnumerable<DomainEvent>(nameof(Events));
            Next = info.GetValue<SignedVersion>(nameof(Next));
            Previous = info.GetValue<SignedVersion>(nameof(Previous));
        }

        public Reference Aggregate { get; }

        public IEnumerable<DomainEvent> Events { get; }

        public SignedVersion Next { get; }

        public SignedVersion Previous { get; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            _ = info.TryAddValue(nameof(Aggregate), Aggregate);
            _ = info.TryAddEnumerable(nameof(Events), Events);
            info.AddValue(nameof(Next), Next);
            info.AddValue(nameof(Previous), Previous);
        }
    }
}