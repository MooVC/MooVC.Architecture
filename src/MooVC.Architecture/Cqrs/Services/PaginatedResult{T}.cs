namespace MooVC.Architecture.Cqrs.Services;

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
        ulong total,
        IEnumerable<T> values)
        : base(context, values)
    {
        Pages = CalculateTotalPages(paging, total);
        Total = total;
    }

    protected PaginatedResult(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        Pages = info.TryGetValue<ushort>(nameof(Pages));
        Total = info.TryGetValue<ulong>(nameof(Total));
    }

    public ushort Pages { get; }

    public ulong Total { get; }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);

        _ = info.TryAddValue(nameof(Pages), Pages);
        _ = info.TryAddValue(nameof(Total), Total);
    }

    internal static ushort CalculateTotalPages(Paging paging, ulong totalResults)
    {
        _ = ArgumentNotNull(
            paging,
            nameof(paging),
            PaginatedResultCalculateTotalPagesPagingRequired);

        decimal requiredPages = (decimal)totalResults / paging.Size;
        ulong totalPages = (ulong)Math.Ceiling(requiredPages);

        return (ushort)Math.Min(totalPages, ushort.MaxValue);
    }
}