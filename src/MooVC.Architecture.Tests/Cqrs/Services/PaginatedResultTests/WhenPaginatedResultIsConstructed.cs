namespace MooVC.Architecture.Cqrs.Services.PaginatedResultTests;

using System;
using MooVC.Architecture.Cqrs.Services.PaginatedQueryTests;
using MooVC.Linq;
using Xunit;

public sealed class WhenPaginatedResultIsConstructed
{
    [Fact]
    public void GivenANullQueryThenAnArgumentNullException()
    {
        PaginatedQuery? query = default;

        ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() =>
            new SerializablePaginatedResult<PaginatedQuery, int>(query!, 0, Array.Empty<int>()));

        Assert.Equal(nameof(query), exception.ParamName);
    }

    [Theory]
    [InlineData(new int[0], ushort.MinValue, ushort.MinValue, ulong.MinValue)]
    [InlineData(new[] { 1, 2, 3 }, 5, 1, 3)]
    [InlineData(default, ushort.MinValue, ushort.MaxValue, ushort.MaxValue)]
    [InlineData(new[] { 4 }, 10, 5, 50)]
    [InlineData(new[] { -100, -200 }, 100, 3, 202)]
    public void GivenQueryResultsAndTotalResultsThenTheQueryResultsAndTotalResultsPropertiesAreSetToMatch(
        int[] values,
        ushort size,
        ushort pages,
        ulong total)
    {
        var paging = new Paging(size: size);
        var query = new SerializablePaginatedQuery(paging);
        var result = new SerializablePaginatedResult<SerializablePaginatedQuery, int>(query, total, values);

        int[] expected = values ?? Array.Empty<int>();

        Assert.Equal(query, result.Query);
        Assert.Equal(expected, result.Value);
        Assert.Equal(pages, result.Pages);
        Assert.Equal(total, result.Total);
    }
}