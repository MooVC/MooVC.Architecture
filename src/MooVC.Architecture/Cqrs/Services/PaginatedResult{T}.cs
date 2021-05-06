namespace MooVC.Architecture.Cqrs.Services
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using MooVC.Collections.Generic;
    using MooVC.Linq;
    using MooVC.Serialization;
    using static MooVC.Architecture.Cqrs.Services.Resources;
    using static MooVC.Ensure;

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
            Results = info.TryGetEnumerable<T>(nameof(Results));
            TotalPages = info.TryGetValue<ushort>(nameof(TotalPages));
            TotalResults = info.TryGetValue<ulong>(nameof(TotalResults));
        }

        public IEnumerable<T> Results { get; }

        public ushort TotalPages { get; }

        public ulong TotalResults { get; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            _ = info.TryAddEnumerable(nameof(Results), Results);
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