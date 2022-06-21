namespace MooVC.Architecture.Cqrs.Services.PaginatedQueryTests;

using System;
using MooVC.Architecture.MessageTests;
using MooVC.Linq;
using Xunit;

public sealed class WhenPaginatedQueryIsConstructed
{
    [Fact]
    public void GivenAContextAndPagingThenThePagingPropertyIsSetToMatch()
    {
        var paging = new Paging();
        var context = new SerializableMessage();
        var query = new SerializablePaginatedQuery(context, paging);

        Assert.Equal(context.CorrelationId, query.CorrelationId);
        Assert.Equal(context.Id, query.CausationId);
        Assert.Equal(paging, query.Paging);
    }

    [Fact]
    public void GivenAContextAndNullPagingThenAnArgumentNullExceptionIsThrown()
    {
        var context = new SerializableMessage();
        Paging? paging = default;

        ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
            () => new SerializablePaginatedQuery(context, paging!));

        Assert.Equal(nameof(paging), exception.ParamName);
    }

    [Fact]
    public void GivenANullPagingThenAnArgumentNullExceptionIsThrown()
    {
        Paging? paging = default;

        ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
            () => new SerializablePaginatedQuery(paging!));

        Assert.Equal(nameof(paging), exception.ParamName);
    }

    [Fact]
    public void GivenANullContextAndPagingThenAnArgumentNullExceptionIsThrown()
    {
        Message? context = default;
        var paging = new Paging();

        ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
            () => new SerializablePaginatedQuery(context!, paging));

        Assert.Equal(nameof(context), exception.ParamName);
    }

    [Fact]
    public void GivenPagingThenThePagingPropertyIsSetToMatch()
    {
        var expected = new Paging();
        var query = new SerializablePaginatedQuery(expected);

        Assert.Equal(expected, query.Paging);
    }
}