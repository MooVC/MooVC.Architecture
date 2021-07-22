namespace MooVC.Architecture.Ddd.Collections
{
    using System.Collections.Generic;

    public static partial class DictionaryExtensions
    {
        public static IDictionary<Reference<TAggregate>, TAggregate> ToUnversioned<TAggregate>(
            this IDictionary<Reference<TAggregate>, TAggregate> aggregates)
            where TAggregate : AggregateRoot
        {
            return new UnversionedReferenceDictionary<TAggregate, TAggregate>(
                existing: aggregates);
        }

        public static IDictionary<Reference<TAggregate>, TProjection> ToUnversioned<TAggregate, TProjection>(
            this IDictionary<Reference<TAggregate>, TProjection> projections)
            where TAggregate : AggregateRoot
            where TProjection : Projection<TAggregate>
        {
            return new UnversionedReferenceDictionary<TAggregate, TProjection>(
                existing: projections);
        }
    }
}