namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using static MooVC.Ensure;
    using static Resources;

    public abstract class AggregateEventArgs<TAggregate>
        : EventArgs
        where TAggregate : AggregateRoot
    {
        protected AggregateEventArgs(TAggregate aggregate)
        {
            ArgumentNotNull(aggregate, nameof(aggregate), GenericAggregateRequired);

            Aggregate = aggregate;
        }

        public TAggregate Aggregate { get; }
    }
}