namespace MooVC.Architecture.Cqrs.Services.PaginatedQueryTests
{
    using MooVC.Linq;
    using MooVC.Serialization;
    using Xunit;

    public sealed class WhenPaginatedQueryIsSerialized
    {
        [Fact]
        public void GivenAnInstanceThenAllPropertiesAreSerialized()
        {
            var query = new PaginatedQuery(new Paging());
            PaginatedQuery deserialized = query.Clone();

            Assert.Equal(query, deserialized);
            Assert.NotSame(query, deserialized);
            Assert.Equal(query.GetHashCode(), deserialized.GetHashCode());
        }
    }
}