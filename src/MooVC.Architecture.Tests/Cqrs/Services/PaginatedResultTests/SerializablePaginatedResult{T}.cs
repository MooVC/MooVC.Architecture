namespace MooVC.Architecture.Cqrs.Services.PaginatedResultTests
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using MooVC.Linq;

    [Serializable]
    internal sealed class SerializablePaginatedResult<T>
        : PaginatedResult<T>
    {
        public SerializablePaginatedResult(Message context, Paging paging, IEnumerable<T> results, ulong totalResults)
            : base(context, paging, results, totalResults)
        {
        }

        private SerializablePaginatedResult(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}