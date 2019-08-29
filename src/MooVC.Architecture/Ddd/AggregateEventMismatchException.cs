namespace MooVC.Architecture.Ddd
{
    using System;
    using static Resources;

    [Serializable]
    public sealed class AggregateEventMismatchException
        : ArgumentException
    {
        public AggregateEventMismatchException(string aggregateName, Reference aggregate, Reference eventAggregate)
            : base(string.Format(
                AggregateEventMismatchExceptionMessage,
                aggregate.Id,
                aggregate.Type.Name,
                eventAggregate.Id,
                eventAggregate.Type.Name))
        {
            Aggregate = aggregate;
            AggregateName = aggregateName;
            EventAggregate = eventAggregate;
        }

        public Reference Aggregate { get; }

        public string AggregateName { get; }

        public Reference EventAggregate { get; }
    }
}