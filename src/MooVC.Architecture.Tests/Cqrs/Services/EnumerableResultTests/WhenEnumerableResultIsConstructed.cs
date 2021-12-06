namespace MooVC.Architecture.Cqrs.Services.EnumerableResultTests
{
    using System;
    using MooVC.Architecture.MessageTests;
    using Xunit;

    public sealed class WhenEnumerableResultIsConstructed
    {
        [Fact]
        public void GivenANullContextThenAnArgumentNullExceptionIsThrown()
        {
            _ = Assert.Throws<ArgumentNullException>(
                () => new SerializableEnumerableResult<int>(default!, Array.Empty<int>()));
        }

        [Theory]
        [InlineData(new int[0])]
        [InlineData(new[] { 1, 2, 3 })]
        [InlineData(default)]
        [InlineData(new[] { 4 })]
        [InlineData(new[] { -100, -200 })]
        public void GivenContextAndValuesThenTheContextAndValuePropertiesAreSetToMatch(int[] values)
        {
            var context = new SerializableMessage();
            var result = new SerializableEnumerableResult<int>(context, values);

            int[] expected = values ?? Array.Empty<int>();

            Assert.Equal(context.Id, result.CausationId);
            Assert.Equal(context.CorrelationId, result.CorrelationId);
            Assert.Equal(expected, result.Value);
        }
    }
}