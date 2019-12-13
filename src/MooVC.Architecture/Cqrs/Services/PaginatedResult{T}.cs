namespace MooVC.Architecture.Cqrs.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using MooVC.Collections.Generic;
    using MooVC.Linq;
    using static MooVC.Ensure;
    using static Resources;

    [Serializable]
    public abstract class PaginatedResult<T>
        : Message
    {
        protected PaginatedResult(Message context, Paging paging, IEnumerable<T> results, ulong totalResults)
            : base(context)
        {
            Results = results.Snapshot();
            TotalPages = CalculateTotalPages(paging, totalResults);
            TotalResults = totalResults;
        }

        protected PaginatedResult(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Results = (T[])info.GetValue(nameof(Results), typeof(T[]));
            TotalPages = (ushort)info.GetValue(nameof(TotalPages), typeof(ushort));
            TotalResults = (ulong)info.GetValue(nameof(TotalResults), typeof(ulong));
        }

        public IEnumerable<T> Results { get; }

        public ushort TotalPages { get; }

        public ulong TotalResults { get; }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(nameof(Results), Results.ToArray());
            info.AddValue(nameof(TotalPages), TotalPages);
            info.AddValue(nameof(TotalResults), TotalResults);
        }

        internal static ushort CalculateTotalPages(Paging paging, ulong totalResults)
        {
            ArgumentNotNull(paging, nameof(paging), PaginatedResultPagingRequired);

            decimal requiredPages = (decimal)totalResults / paging.Size;
            ulong totalPages = (ulong)Math.Ceiling(requiredPages);

            return (ushort)Math.Min(totalPages, ushort.MaxValue);
        }
    }
}