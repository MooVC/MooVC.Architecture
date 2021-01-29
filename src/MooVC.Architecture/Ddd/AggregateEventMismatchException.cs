namespace MooVC.Architecture.Ddd
{
    using System;
    using static System.String;
    using static MooVC.Architecture.Ddd.Resources;

    [Serializable]
    public sealed class AggregateEventMismatchException
        : ArgumentException
    {
        public AggregateEventMismatchException(VersionedReference aggregate, VersionedReference eventAggregate)
            : base(Format(
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