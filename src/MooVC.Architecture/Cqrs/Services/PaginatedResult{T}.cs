namespace MooVC.Architecture.Cqrs.Services
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using MooVC.Linq;
    using MooVC.Serialization;
    using static MooVC.Architecture.Cqrs.Services.Resources;
    using static MooVC.Ensure;

    [Serializable]
    public abstract class PaginatedResult<T>
        : EnumerableResult<T>
    {
        protected PaginatedResult(
            Message context,
            Paging paging,
            IEnumerable<T> results,
            ulong totalResults)
            : base(context, results)
        {
            TotalPages = CalculateTotalPages(paging, totalResults);
            TotalResults = totalResults;
        }

        protected PaginatedResult(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            TotalPages = info.TryGetValue<ushort>(nameof(TotalPages));
            TotalResults = info.TryGetValue<ulong>(nameof(TotalResults));
        }

        public ushort TotalPages { get; }

        public ulong TotalResults { get; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            _ = info.TryAddValue(nameof(TotalPages), TotalPages);
            _ = info.TryAddValue(nameof(TotalResults), TotalResults);
        }

        internal static ushort CalculateTotalPages(Paging paging, ulong totalResults)
        {
            ArgumentNotNull(paging, nameof(paging), PaginatedResultCalculateTotalPagesPagingRequired);

            decimal requiredPages = (decimal)totalResults / paging.Size;
            ulong totalPages = (ulong)Math.Ceiling(requiredPages);

            return (ushort)Math.Min(totalPages, ushort.MaxValue);
        }
    }
}