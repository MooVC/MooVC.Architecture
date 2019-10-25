namespace MooVC.Architecture.Ddd
{
    using System;
    using static Resources;

    [Serializable]
    public sealed class AggregateEventMismatchException
        : ArgumentException
    {
        public AggregateEventMismatchException(string aggregateName, VersionedReference aggregate, VersionedReference eventAggregate)
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
            AggregateName = aggregateName;
            EventAggregate = eventAggregate;
        }

        public VersionedReference Aggregate { get; }

        public string AggregateName { get; }

        public VersionedReference EventAggregate { get; }
    }
}