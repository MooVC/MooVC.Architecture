namespace MooVC.Architecture.Cqrs.Services
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using MooVC.Serialization;

    [Serializable]
    public abstract class PaginatedResult<TQuery, T>
        : PaginatedResult<T>
        where TQuery : PaginatedQuery
    {
        protected PaginatedResult(TQuery query, IEnumerable<T> results, ulong totalResults)
            : base(query, query.Paging, results, totalResults)
        {
            Query = query;
        }

        protected PaginatedResult(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Query = info.GetValue<TQuery>(nameof(Query));
        }

        public TQuery Query { get; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(nameof(Query), Query);
        }
    }
}