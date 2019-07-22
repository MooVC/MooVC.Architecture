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

    [Serializable]
    public class PaginatedResult<T>
        : Message
    {
        private readonly Lazy<ushort> totalPages;

        public PaginatedResult(Paging paging, IEnumerable<T> results, ulong totalResults)
        {
            ArgumentNotNull(paging, nameof(Paging), Resources.PaginatedResultPagingRequired);

            Paging = paging;
            Results = results.Snapshot();
            TotalResults = totalResults;

            totalPages = new Lazy<ushort>(() => CalculateTotalPages());
        }

        public PaginatedResult(Message context, Paging paging, IEnumerable<T> results, ulong totalResults)
            : base(context)
        {
            ArgumentNotNull(paging, nameof(Paging), Resources.PaginatedResultPagingRequired);

            Paging = paging;
            Results = results.Snapshot();
            TotalResults = totalResults;

            totalPages = new Lazy<ushort>(() => CalculateTotalPages());
        }

        protected PaginatedResult(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Paging = (Paging)info.GetValue(nameof(Paging), typeof(Paging));
            Results = (T[])info.GetValue(nameof(Results), typeof(T[]));
            TotalResults = (ulong)info.GetValue(nameof(TotalResults), typeof(ulong));

            totalPages = new Lazy<ushort>(() => CalculateTotalPages());
        }

        public Paging Paging { get; }

        public IEnumerable<T> Results { get; }

        public ushort TotalPages => totalPages.Value;

        public ulong TotalResults { get; }
        
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(nameof(Paging), Paging);
            info.AddValue(nameof(Results), Results.ToArray());
            info.AddValue(nameof(TotalResults), TotalResults);
        }

        private ushort CalculateTotalPages()
        {
            decimal pages = (decimal)TotalResults / Paging.Size;

            return (ushort)Math.Ceiling(pages);
        }
    }
}