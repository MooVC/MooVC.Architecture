namespace MooVC.Architecture.Cqrs.Services.PaginatedResultTests
{
    using System;
    using MooVC.Linq;
    using Xunit;

    public sealed class WhenPaginatedResultIsConstructed
    {
        [Theory]
        [InlineData(ushort.MinValue, new int[0], ushort.MinValue, ulong.MinValue)]
        [InlineData(ushort.MaxValue, new[] { 1, 2, 3 }, ushort.MaxValue, ulong.MaxValue)]
        [InlineData(ushort.MinValue, null, ushort.MinValue, ulong.MaxValue)]
        [InlineData(ushort.MinValue, new[] { 4 }, ushort.MaxValue, ulong.MinValue)]
        [InlineData(ushort.MaxValue, new[] { -100, -200 }, ushort.MinValue, ulong.MinValue)]
        public void GivenQueryResultsAndTotalResultsThenTheQueryResultsAndTotalResultsPropertiesAreSetToMatch(
            ushort page, 
            int[] results, 
            ushort size, 
            ulong totalResults)
        {
            var paging = new Paging(page: page, size: size);
            var query = new PaginatedQuery(paging);
            var result = new PaginatedResult<PaginatedQuery, int>(query, results, totalResults);

            int[] expectedResults = results ?? new int[0];

            Assert.Equal(query, result.Query);
            Assert.Equal(expectedResults, result.Results);
            Assert.Equal(totalResults, result.TotalResults);
        }

        [Fact]
        public void GivenNullQueryThenAnArgumentNullExceptionIsThrown()
        {
            _ = Assert.Throws<ArgumentNullException>(() => new PaginatedResult<PaginatedQuery, int>(null, new int[0], 0));
        }
    }
}