namespace MooVC.Architecture.Cqrs.Services.PaginatedResultTests
{
    using MooVC.Linq;
    using Xunit;

    public sealed class WhenTotalPagesIsCalled
    {
        [Theory]
        [InlineData(10, 100, 10)]
        [InlineData(1, 100, 100)]
        [InlineData(5, 99, 20)]
        [InlineData(5, 96, 20)]
        public void GivenPagingAndTotalResultsThenTheTotalPagesIsEnoughToCoverAllResults(ushort size, ulong totalResults, ushort expectedPages)
        {
            var paging = new Paging(size: size);
            var query = new PaginatedQuery(paging);
            var result = new PaginatedResult<PaginatedQuery, int>(query, new int[0], totalResults);

            Assert.Equal(expectedPages, result.TotalPages);
        }
    }
}