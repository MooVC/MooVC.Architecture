namespace MooVC.Architecture.Ddd.Collections;

using System.Collections.Generic;
using System.Linq;

public static partial class EnumerableExtensions
{
    public static IEnumerable<Reference<TAggregate>> ToReferences<TAggregate>(this IEnumerable<TAggregate>? aggregates, bool unversioned = false)
        where TAggregate : AggregateRoot
    {
        if (aggregates is null)
        {
            return Enumerable.Empty<Reference<TAggregate>>();
        }

        return aggregates
            .Select(aggregate => aggregate.ToReference(unversioned: unversioned))
            .ToArray();
    }
}