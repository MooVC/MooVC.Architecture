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

    /// <summary>
    /// Populates the specified <see cref="SerializationInfo"/> object with the data needed to serialize the current instance
    /// of the <see cref="Paging"/> class.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo"/> object that will be populated with data.</param>
    /// <param name="context">The destination (see <see cref="StreamingContext"/>) for the serialization operation.</param>
    [Obsolete(@"Slated for removal in v11 as part of Microsoft's BinaryFormatter Obsoletion Strategy.
                       (see: https://github.com/dotnet/designs/blob/main/accepted/2020/better-obsoletion/binaryformatter-obsoletion.md)")]
    protected PaginatedResult(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        ushort pages = info.TryGetValue<ushort>(nameof(Pages));
        Total = info.TryGetValue<ulong>(nameof(Total));

        this.pages = new(pages);
    }

    public ushort Pages => pages.Value;

    public ulong Total { get; }

    /// <summary>
    /// Populates the specified <see cref="SerializationInfo"/> object with the data needed to serialize the current instance
    /// of the <see cref="Paging"/> class.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo"/> object that will be populated with data.</param>
    /// <param name="context">The destination (see <see cref="StreamingContext"/>) for the serialization operation.</param>
    [Obsolete(@"Slated for removal in v11 as part of Microsoft's BinaryFormatter Obsoletion Strategy.
                       (see: https://github.com/dotnet/designs/blob/main/accepted/2020/better-obsoletion/binaryformatter-obsoletion.md)")]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);

        _ = info.TryAddValue(nameof(Pages), Pages);
        _ = info.TryAddValue(nameof(Total), Total);
    }

    internal static ushort CalculateTotalPages(Paging paging, ulong totalResults)
    {
        _ = IsNotNull(paging, message: PaginatedResultCalculateTotalPagesPagingRequired);

        decimal requiredPages = (decimal)totalResults / paging.Size;
        ulong totalPages = (ulong)Math.Ceiling(requiredPages);

        return (ushort)Math.Min(totalPages, ushort.MaxValue);
    }
}