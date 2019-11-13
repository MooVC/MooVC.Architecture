namespace MooVC.Architecture.Cqrs.Services.PaginatedResultTests
{
    using MooVC.Architecture.MessageTests;
    using MooVC.Linq;
    using MooVC.Serialization;
    using Xunit;

    public sealed class WhenPaginatedResultIsSerialized
    {
        [Fact]
        public void GivenANonQueryTypedInstanceThenAllPropertiesAreSerialized()
        {
            var context = new SerializableMessage();
            var result = new PaginatedResult<int>(context, new Paging(), new[] { 1, 2, 3 }, 100);
            PaginatedResult<int> deserialized = result.Clone();

            Assert.Equal(result, deserialized);
            Assert.NotSame(result, deserialized);
            Assert.Equal(result.GetHashCode(), deserialized.GetHashCode());
        }

        [Fact]
        public void GivenAQueryTypedInstanceThenAllPropertiesAreSerialized()
        {
            var result = new PaginatedResult<PaginatedQuery, int>(new PaginatedQuery(new Paging()), new[] { 1, 2, 3 }, 100);
            PaginatedResult<PaginatedQuery, int> deserialized = result.Clone();

            Assert.Equal(result, deserialized);
            Assert.NotSame(result, deserialized);
            Assert.Equal(result.GetHashCode(), deserialized.GetHashCode());
        }
    }
}