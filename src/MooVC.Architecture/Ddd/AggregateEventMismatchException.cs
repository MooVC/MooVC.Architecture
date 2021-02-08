namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Runtime.Serialization;
    using MooVC.Architecture.Serialization;
    using static System.String;
    using static MooVC.Architecture.Ddd.Resources;
    using static MooVC.Ensure;

    [Serializable]
    public sealed class AggregateEventMismatchException
        : ArgumentException
    {
        public AggregateEventMismatchException(VersionedReference aggregate, VersionedReference eventAggregate)
            : base(FormatMessage(aggregate, eventAggregate))
        {
            Aggregate = aggregate;
            EventAggregate = eventAggregate;
        }

        private AggregateEventMismatchException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Aggregate = info.TryGetVersionedReference(nameof(Aggregate));
            EventAggregate = info.TryGetVersionedReference(nameof(EventAggregate));
        }

        public VersionedReference Aggregate { get; }

        public VersionedReference EventAggregate { get; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            _ = info.TryAddVersionedReference(nameof(Aggregate), Aggregate);
            _ = info.TryAddVersionedReference(nameof(EventAggregate), EventAggregate);
        }

        private static string FormatMessage(VersionedReference aggregate, VersionedReference eventAggregate)
        {
            ArgumentNotNull(aggregate, nameof(aggregate), AggregateEventMismatchExceptionAggregateRequired);
            ArgumentNotNull(eventAggregate, nameof(eventAggregate), AggregateEventMismatchExceptionEventAggregateRequired);

            return Format(
                AggregateEventMismatchExceptionMessage,
                aggregate.Id,
                aggregate.Type.Name,
                aggregate.Version,
                eventAggregate.Id,
                eventAggregate.Type.Name,
                eventAggregate.Version);
        }
    }
}