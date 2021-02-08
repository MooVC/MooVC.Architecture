namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using MooVC.Architecture.Serialization;
    using MooVC.Collections.Generic;
    using MooVC.Serialization;
    using static System.String;
    using static MooVC.Architecture.Ddd.Resources;
    using static MooVC.Ensure;

    [Serializable]
    public sealed class AggregateEventSequenceUnorderedException
        : ArgumentException
    {
        internal AggregateEventSequenceUnorderedException(VersionedReference aggregate, IEnumerable<DomainEvent> events)
            : base(FormatMessage(aggregate, events))
        {
            Aggregate = aggregate;
            Events = events.Snapshot();
        }

        private AggregateEventSequenceUnorderedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Aggregate = info.TryGetVersionedReference(nameof(Aggregate));
            Events = info.TryGetEnumerable<DomainEvent>(nameof(Events));
        }

        public VersionedReference Aggregate { get; }

        public IEnumerable<DomainEvent> Events { get; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            _ = info.TryAddVersionedReference(nameof(Aggregate), Aggregate);
            _ = info.TryAddEnumerable(nameof(Events), Events);
        }

        private static string FormatMessage(VersionedReference aggregate, IEnumerable<DomainEvent> events)
        {
            ArgumentNotNull(aggregate, nameof(aggregate), AggregateEventSequenceUnorderedExceptionAggregateRequired);
            ArgumentNotNull(events, nameof(events), AggregateEventSequenceUnorderedExceptionEventsRequired);

            return Format(
                AggregateEventSequenceUnorderedExceptionMessage,
                aggregate.Id,
                aggregate.Version,
                aggregate.Type.Name);
        }
    }
}