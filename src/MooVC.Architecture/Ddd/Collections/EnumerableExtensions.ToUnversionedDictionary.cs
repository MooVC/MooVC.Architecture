namespace MooVC.Architecture.Ddd.Collections;

using System.Collections.Generic;
using System.Linq;

public static partial class EnumerableExtensions
{
    public static IDictionary<Reference<TAggregate>, TAggregate> ToUnversionedDictionary<TAggregate>(
        this IEnumerable<TAggregate> aggregates)
        where TAggregate : AggregateRoot
    {
        var aggregated = aggregates.ToDictionary(aggregate => aggregate.ToReference(unversioned: true), aggregate => aggregate);

        return new UnversionedReferenceDictionary<TAggregate, TAggregate>(existing: aggregated);
    }

    public static IDictionary<Reference<TAggregate>, TProjection> ToUnversionedDictionary<TAggregate, TProjection>(
        this IEnumerable<TProjection> projections)
        where TAggregate : AggregateRoot
        where TProjection : Projection<TAggregate>
    {
        var aggregated = projections.ToDictionary(projection => projection.Aggregate, projection => projection);

        return new UnversionedReferenceDictionary<TAggregate, TProjection>(existing: aggregated);
    }
}