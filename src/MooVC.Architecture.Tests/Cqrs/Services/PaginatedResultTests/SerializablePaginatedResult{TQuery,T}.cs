namespace MooVC.Architecture.Cqrs.Services.PaginatedResultTests
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [Serializable]
    internal sealed class SerializablePaginatedResult<TQuery, T>
        : PaginatedResult<TQuery, T>
        where TQuery : PaginatedQuery
    {
        public SerializablePaginatedResult(
            TQuery query,
            IEnumerable<T> results,
            ulong totalResults)
            : base(query, results, totalResults)
        {
        }

        private SerializablePaginatedResult(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}