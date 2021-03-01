namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MooVC.Collections.Generic;

    internal static partial class DictionaryExtensions
    {
        public static IEnumerable<TAggregate> GetLatestVersions<TAggregate>(
            this IDictionary<Reference<TAggregate>, TAggregate> source,
            Guid? aggregateId = default)
            where TAggregate : AggregateRoot
        {
            return source
                .Values
                .GroupBy(aggregate => aggregate.Id)
                .WhereIf(aggregateId.HasValue, aggregate => aggregate.Key == aggregateId)
                .Select(aggregate => aggregate
                    .OrderBy(aggregate => aggregate.Version)
                    .Last())
                .ToArray();
        }
    }
}