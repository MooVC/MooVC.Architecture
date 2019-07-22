namespace MooVC.Architecture.Cqrs.Services.PaginatedQueryTests
{
    using System;
    using MooVC.Linq;
    using Xunit;

    public sealed class WhenPaginatedQueryIsConstructed
    {
        [Fact]
        public void GivenPagingThenThePagingPropertyIsSetToMatch()
        {
            var expected = new Paging();
            var query = new PaginatedQuery(expected);

            Assert.Equal(expected, query.Paging);
        }

        [Fact]
        public void GivenNullPagingThenAnArgumentNullExceptionIsThrown()
        {
            _ = Assert.Throws<ArgumentNullException>(() => new PaginatedQuery(null));
        }
    }
}