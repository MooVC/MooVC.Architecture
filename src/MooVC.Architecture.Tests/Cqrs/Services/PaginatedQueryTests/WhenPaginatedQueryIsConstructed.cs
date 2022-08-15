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
        var query = new SerializablePaginatedQuery(context: context, paging: paging);

        Assert.Equal(context.CorrelationId, query.CorrelationId);
        Assert.Equal(context.Id, query.CausationId);
        Assert.Equal(paging, query.Paging);
    }

    [Fact]
    public void GivenAContextAndNullPagingThenPagingIsSetToDefault()
    {
        Paging? paging = default;
        var context = new SerializableMessage();
        var query = new SerializablePaginatedQuery(context: context, paging: paging);

        Assert.Equal(context.CorrelationId, query.CorrelationId);
        Assert.Equal(context.Id, query.CausationId);
        Assert.Equal(Paging.Default, query.Paging);
    }

    [Fact]
    public void GivenANullPagingThenPagingIsSetToDefault()
    {
        Paging? paging = default;
        var query = new SerializablePaginatedQuery(paging: paging);

        Assert.Equal(Paging.Default, query.Paging);
    }

    [Fact]
    public void GivenPagingThenThePagingPropertyIsSetToMatch()
    {
        var expected = new Paging();
        var query = new SerializablePaginatedQuery(expected);

        Assert.Equal(expected, query.Paging);
    }
}