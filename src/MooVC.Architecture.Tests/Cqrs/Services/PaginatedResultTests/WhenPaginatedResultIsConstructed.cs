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
                () => new SerializablePaginatedResult<int>(
                    default!,
                    new Paging(),
                    0,
                    Array.Empty<int>()));
        }

        [Fact]
        public void GivenANullQueryThenANullReferenceExceptionIsThrown()
        {
            _ = Assert.Throws<NullReferenceException>(
                () => new SerializablePaginatedResult<PaginatedQuery, int>(
                    default!,
                    0,
                    Array.Empty<int>()));
        }

        [Fact]
        public void GivenNullPagingThenAnArgumentNullExceptionIsThrown()
        {
            _ = Assert.Throws<ArgumentNullException>(
                () => new SerializablePaginatedResult<int>(
                    new SerializableMessage(),
                    default!,
                    0,
                    Array.Empty<int>()));
        }

        [Theory]
        [InlineData(new int[0], ushort.MinValue, ushort.MinValue, ulong.MinValue)]
        [InlineData(new[] { 1, 2, 3 }, 5, 1, 3)]
        [InlineData(default, ushort.MinValue, ushort.MaxValue, ushort.MaxValue)]
        [InlineData(new[] { 4 }, 10, 5, 50)]
        [InlineData(new[] { -100, -200 }, 100, 3, 202)]
        public void GivenContextResultsAndTotalResultsThenTheContextResultsAndTotalResultsPropertiesAreSetToMatch(
            int[] values,
            ushort size,
            ushort pages,
            ulong total)
        {
            var context = new SerializableMessage();
            var paging = new Paging(size: size);
            var result = new SerializablePaginatedResult<int>(context, paging, total, values);

            int[] expected = values ?? Array.Empty<int>();

            Assert.Equal(context.Id, result.CausationId);
            Assert.Equal(context.CorrelationId, result.CorrelationId);
            Assert.Equal(expected, result.Value);
            Assert.Equal(pages, result.Pages);
            Assert.Equal(total, result.Total);
        }

        [Theory]
        [InlineData(new int[0], ushort.MinValue, ushort.MinValue, ulong.MinValue)]
        [InlineData(new[] { 1, 2, 3 }, 5, 1, 3)]
        [InlineData(default, ushort.MinValue, ushort.MaxValue, ushort.MaxValue)]
        [InlineData(new[] { 4 }, 10, 5, 50)]
        [InlineData(new[] { -100, -200 }, 100, 3, 202)]
        public void GivenQueryResultsAndTotalResultsThenTheQueryResultsAndTotalResultsPropertiesAreSetToMatch(
            int[] values,
            ushort size,
            ushort pages,
            ulong total)
        {
            var paging = new Paging(size: size);
            var query = new SerializablePaginatedQuery(paging);
            var result = new SerializablePaginatedResult<SerializablePaginatedQuery, int>(query, total, values);

            int[] expected = values ?? Array.Empty<int>();

            Assert.Equal(query, result.Query);
            Assert.Equal(expected, result.Value);
            Assert.Equal(pages, result.Pages);
            Assert.Equal(total, result.Total);
        }
    }
}