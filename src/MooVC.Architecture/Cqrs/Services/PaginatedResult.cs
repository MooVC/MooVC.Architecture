namespace MooVC.Architecture.Cqrs.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using MooVC.Collections.Generic;

    [Serializable]
    public class PaginatedResult<TQuery, T>
        : Message, 
          IPaginatedResult<T> 
        where TQuery : PaginatedQuery
    {
        private readonly Lazy<ushort> totalPages;

        public PaginatedResult(TQuery query, IEnumerable<T> results, ulong totalResults)
            : base(query)
        {
            Query = query;
            Results = results.Snapshot();
            TotalResults = totalResults;

            totalPages = new Lazy<ushort>(() => CalculateTotalPages());
        }

        protected PaginatedResult(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Query = (TQuery)info.GetValue(nameof(Query), typeof(TQuery));
            Results = (T[])info.GetValue(nameof(Results), typeof(T[]));
            TotalResults = (ulong)info.GetValue(nameof(TotalResults), typeof(ulong));

            totalPages = new Lazy<ushort>(() => CalculateTotalPages());
        }

        public TQuery Query { get; }

        public IEnumerable<T> Results { get; }

        public ushort TotalPages => totalPages.Value;

        public ulong TotalResults { get; }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(nameof(Query), Query);
            info.AddValue(nameof(Results), Results.ToArray());
            info.AddValue(nameof(TotalResults), TotalResults);
        }

        private ushort CalculateTotalPages()
        {
            decimal pages = (decimal)TotalResults / Query.Paging.Size;

            return (ushort)Math.Ceiling(pages);
        }
    }
}