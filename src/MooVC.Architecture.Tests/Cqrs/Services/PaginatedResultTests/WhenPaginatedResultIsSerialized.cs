namespace MooVC.Architecture.Cqrs.Services.PaginatedResultTests
{
    using MooVC.Linq;
    using Xunit;

    public sealed class WhenPaginatedResultIsSerialized
    {
        [Fact]
        public void GivenAnInstanceThenAllPropertiesAreSerialized()
        {
            var query = new PaginatedResult<int>(new Paging(), new[] { 1, 2, 3 }, 100);
            PaginatedResult<int> deserialized = query.Serialize();

            Assert.Equal(query, deserialized);
            Assert.NotSame(query, deserialized);
        }
    }
}