namespace MooVC.Architecture.Cqrs.Services.PaginatedResultTests
{
    using MooVC.Linq;
    using MooVC.Serialization;
    using Xunit;

    public sealed class WhenPaginatedResultIsSerialized
    {
        [Fact]
        public void GivenAnInstanceThenAllPropertiesAreSerialized()
        {
            var query = new PaginatedResult<PaginatedQuery, int>(new PaginatedQuery(new Paging()), new[] { 1, 2, 3 }, 100);
            PaginatedResult<PaginatedQuery, int> deserialized = query.Clone();

            Assert.Equal(query, deserialized);
            Assert.NotSame(query, deserialized);
        }
    }
}