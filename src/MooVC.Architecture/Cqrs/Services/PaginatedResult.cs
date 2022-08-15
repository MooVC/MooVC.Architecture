namespace MooVC.Architecture.Cqrs.Services;

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MooVC.Linq;
using MooVC.Serialization;
using static MooVC.Architecture.Cqrs.Services.Resources;
using static MooVC.Ensure;

[Serializable]
public abstract class PaginatedResult<TQuery, T>
    : EnumerableResult<TQuery, T>
    where TQuery : PaginatedQuery
{
    private readonly Lazy<ushort> pages;

    protected PaginatedResult(TQuery query, ulong total, IEnumerable<T> values)
        : base(query, values)
    {
        pages = new(() => CalculateTotalPages(query.Paging, total));
        Total = total;
    }

    protected PaginatedResult(TQuery query, PagedResult<T> result)
        : base(query, result.Values)
    {
        pages = new(() => CalculateTotalPages(query.Paging, result.Total));
        Total = result.Total;
    }

    protected PaginatedResult(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        ushort pages = info.TryGetValue<ushort>(nameof(Pages));
        Total = info.TryGetValue<ulong>(nameof(Total));

        this.pages = new(pages);
    }

    public ushort Pages => pages.Value;

    public ulong Total { get; }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);

        _ = info.TryAddValue(nameof(Pages), Pages);
        _ = info.TryAddValue(nameof(Total), Total);
    }

    internal static ushort CalculateTotalPages(Paging paging, ulong totalResults)
    {
        _ = ArgumentNotNull(paging, nameof(paging), PaginatedResultCalculateTotalPagesPagingRequired);

        decimal requiredPages = (decimal)totalResults / paging.Size;
        ulong totalPages = (ulong)Math.Ceiling(requiredPages);

        return (ushort)Math.Min(totalPages, ushort.MaxValue);
    }
}