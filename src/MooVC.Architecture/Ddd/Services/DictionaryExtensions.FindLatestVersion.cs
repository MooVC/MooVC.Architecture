namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static partial class DictionaryExtensions
    {
        public static Reference<TAggregate>? FindLatestVersion<TAggregate>(
            this IDictionary<Reference<TAggregate>, TAggregate> source,
            Guid aggregateId)
            where TAggregate : AggregateRoot
        {
            return source
                .GetLatestVersions(aggregateId: aggregateId)
                .LastOrDefault()?
                .ToReference();
        }
    }
}