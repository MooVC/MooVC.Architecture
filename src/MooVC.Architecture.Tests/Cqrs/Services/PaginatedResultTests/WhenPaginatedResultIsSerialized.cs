namespace MooVC.Architecture.Cqrs.Services.PaginatedResultTests;

using MooVC.Architecture.Cqrs.Services.PaginatedQueryTests;
using MooVC.Architecture.Serialization;
using MooVC.Linq;
using Xunit;

public sealed class WhenPaginatedResultIsSerialized
{
    [Fact]
    public void GivenAQueryTypedInstanceThenAllPropertiesAreSerialized()
    {
        var result = new SerializablePaginatedResult<SerializablePaginatedQuery, int>(
            new SerializablePaginatedQuery(new Paging()), 100, new[] { 1, 2, 3 });
        SerializablePaginatedResult<SerializablePaginatedQuery, int> deserialized = result.Clone();

        Assert.Equal(result, deserialized);
        Assert.NotSame(result, deserialized);
        Assert.Equal(result.GetHashCode(), deserialized.GetHashCode());
    }
}