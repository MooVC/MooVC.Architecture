namespace MooVC.Architecture.Cqrs.Services
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    [Serializable]
    public class PaginatedResult<TQuery, T>
        : PaginatedResult<T>
        where TQuery : PaginatedQuery
    {
        public PaginatedResult(TQuery query, IEnumerable<T> results, ulong totalResults)
            : base(query, query?.Paging, results, totalResults)
        {
            Query = query;
        }

        protected PaginatedResult(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Query = (TQuery)info.GetValue(nameof(Query), typeof(TQuery));
        }

        public TQuery Query { get; }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(nameof(Query), Query);
        }
    }
}