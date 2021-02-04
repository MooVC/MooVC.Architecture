namespace MooVC.Architecture.Cqrs.Services.PaginatedQueryTests
{
    using MooVC.Architecture.Serialization;
    using MooVC.Linq;
    using MooVC.Serialization;
    using Xunit;

    public sealed class WhenPaginatedQueryIsSerialized
    {
        [Fact]
        public void GivenAnInstanceThenAllPropertiesAreSerialized()
        {
            var query = new SerializablePaginatedQuery(new Paging());
            SerializablePaginatedQuery deserialized = query.Clone();

            Assert.Equal(query, deserialized);
            Assert.NotSame(query, deserialized);
            Assert.Equal(query.GetHashCode(), deserialized.GetHashCode());
        }
    }
}