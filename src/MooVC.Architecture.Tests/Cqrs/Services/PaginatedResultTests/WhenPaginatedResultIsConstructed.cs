namespace MooVC.Architecture.Cqrs.Services.PaginatedResultTests
{
    using System;
    using MooVC.Architecture.Cqrs.Services.PaginatedQueryTests;
    using MooVC.Architecture.MessageTests;
    using MooVC.Linq;
    using Xunit;

    public sealed class WhenPaginatedResultIsConstructed
    {
        [Fact]
        public void GivenANullContextThenAnArgumentNullExceptionIsThrown()
        {
            _ = Assert.Throws<ArgumentNullException>(
                () => new SerializablePaginatedResult<int>(default!, new Paging(), new int[0], 0));
        }

        [Fact]
        public void GivenANullQueryThenANullReferenceExceptionIsThrown()
        {
            _ = Assert.Throws<NullReferenceException>(
                () => new SerializablePaginatedResult<PaginatedQuery, int>(default!, new int[0], 0));
        }

        [Fact]
        public void GivenNullPagingThenAnArgumentNullExceptionIsThrown()
        {
            _ = Assert.Throws<ArgumentNullException>(
                () => new SerializablePaginatedResult<int>(new SerializableMessage(), default!, new int[0], 0));
        }

        [Theory]
        [InlineData(new int[0], ushort.MinValue, ushort.MinValue, ulong.MinValue)]
        [InlineData(new[] { 1, 2, 3 }, 5, 1, 3)]
        [InlineData(default, ushort.MinValue, ushort.MaxValue, ushort.MaxValue)]
        [InlineData(new[] { 4 }, 10, 5, 50)]
        [InlineData(new[] { -100, -200 }, 100, 3, 202)]
        public void GivenContextResultsAndTotalResultsThenTheContextResultsAndTotalResultsPropertiesAreSetToMatch(
            int[] results,
            ushort size,
            ushort totalPages,
            ulong totalResults)
        {
            var context = new SerializableMessage();
            var paging = new Paging(size: size);
            var result = new SerializablePaginatedResult<int>(context, paging, results, totalResults);

            int[] expectedResults = results ?? new int[0];

            Assert.Equal(context.Id, result.CausationId);
            Assert.Equal(context.CorrelationId, result.CorrelationId);
            Assert.Equal(expectedResults, result.Results);
            Assert.Equal(totalPages, result.TotalPages);
            Assert.Equal(totalResults, result.TotalResults);
        }

        [Theory]
        [InlineData(new int[0], ushort.MinValue, ushort.MinValue, ulong.MinValue)]
        [InlineData(new[] { 1, 2, 3 }, 5, 1, 3)]
        [InlineData(default, ushort.MinValue, ushort.MaxValue, ushort.MaxValue)]
        [InlineData(new[] { 4 }, 10, 5, 50)]
        [InlineData(new[] { -100, -200 }, 100, 3, 202)]
        public void GivenQueryResultsAndTotalResultsThenTheQueryResultsAndTotalResultsPropertiesAreSetToMatch(
            int[] results,
            ushort size,
            ushort totalPages,
            ulong totalResults)
        {
            var paging = new Paging(size: size);
            var query = new SerializablePaginatedQuery(paging);
            var result = new SerializablePaginatedResult<SerializablePaginatedQuery, int>(query, results, totalResults);

            int[] expectedResults = results ?? new int[0];

            Assert.Equal(query, result.Query);
            Assert.Equal(expectedResults, result.Results);
            Assert.Equal(totalPages, result.TotalPages);
            Assert.Equal(totalResults, result.TotalResults);
        }
    }
}