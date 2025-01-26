namespace MooVC.Architecture.Ddd.Collections;

using System.Collections.Generic;
using System.Linq;

public static partial class EnumerableExtensions
{
    public static IEnumerable<Reference> ToUnversioned(this IEnumerable<Reference>? references)
    {
        if (references is null)
        {
            return [];
        }

        return references
            .Select(reference => reference.ToUnversioned())
            .ToArray();
    }

    public static IEnumerable<Reference<TAggregate>> ToUnversioned<TAggregate>(this IEnumerable<Reference<TAggregate>>? references)
        where TAggregate : AggregateRoot
    {
        if (references is null)
        {
            return [];
        }

        return references
            .Select(reference => reference.ToUnversioned())
            .ToArray();
    }
}