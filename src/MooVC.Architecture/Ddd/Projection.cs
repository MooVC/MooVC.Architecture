namespace MooVC.Architecture.Ddd
{
    using static MooVC.Architecture.Ddd.Ensure;
    using static Resources;

    public abstract class Projection<TAggregate>
        where TAggregate : AggregateRoot
    {
        protected Projection(TAggregate aggregate)
            : this(aggregate.ToVersionedReference())
        {
        }

        protected Projection(VersionedReference<TAggregate> aggregate)
        {
            ReferenceIsNotEmpty(aggregate, nameof(aggregate), ProjectionAggregateRequired);

            Aggregate = aggregate;
        }

        public VersionedReference<TAggregate> Aggregate { get; }
    }
}