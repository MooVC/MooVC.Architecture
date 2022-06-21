namespace MooVC.Architecture.Ddd.Services;

using System;
using System.Collections.Generic;

internal static partial class DictionaryExtensions
{
    public static Reference<TAggregate> DetermineVersionedKey<TAggregate>(
        this IDictionary<Reference<TAggregate>, TAggregate> source,
        Guid aggregateId,
        SignedVersion? version)
        where TAggregate : AggregateRoot
    {
        Reference<TAggregate>? key = default;

        if (version is null || version.IsEmpty)
        {
            key = source.FindLatestVersion(aggregateId);
        }

        return key ?? aggregateId.ToReference<TAggregate>(version: version);
    }
}