namespace MooVC.Architecture.Ddd
{
    using System;
    using static Resources;

    [Serializable]
    public sealed class AggregateEventMismatchException
        : ArgumentException
    {
        public AggregateEventMismatchException(VersionedReference aggregate, VersionedReference eventAggregate)
            : base(string.Format(
                AggregateEventMismatchExceptionMessage,
                aggregate.Id,
                aggregate.Type.Name,
                aggregate.Version,
                eventAggregate.Id,
                eventAggregate.Type.Name,
                eventAggregate.Version))
        {
            Aggregate = aggregate;
            EventAggregate = eventAggregate;
        }

        public VersionedReference Aggregate { get; }

        public VersionedReference EventAggregate { get; }
    }
}