namespace MooVC.Architecture.Ddd
{
    using System;

    [Serializable]
    public sealed class AggregateEventMismatchException
        : ArgumentException
    {
        public AggregateEventMismatchException(string aggregateName, IReference aggregate, IReference eventAggregate)
            : base(string.Format(
                Resources.AggregateEventMismatchExceptionMessage,
                aggregate.Id,
                aggregate.Type.Name,
                eventAggregate.Id,
                eventAggregate.Type.Name))
        {
            Aggregate = aggregate;
            AggregateName = aggregateName;
            EventAggregate = eventAggregate;
        }

        public IReference Aggregate { get; }

        public string AggregateName { get; }

        public IReference EventAggregate { get; }
    }
}