namespace MooVC.Architecture.Cqrs.Services.EnumerableResultTests
{
    using System;
    using MooVC.Architecture.MessageTests;
    using Xunit;

    public sealed class WhenImplicityCastToValue
    {
        [Theory]
        [InlineData(new int[0])]
        [InlineData(new[] { 1, 2, 3 })]
        [InlineData(default)]
        [InlineData(new[] { 4 })]
        [InlineData(new[] { -100, -200 })]
        public void GivenAResultThenTheValueIsReturned(int[] values)
        {
            var context = new SerializableMessage();
            var result = new SerializableEnumerableResult<int>(context, values);
            int[] actual = result;
            int[] expected = values ?? Array.Empty<int>();

            Assert.Equal(expected, actual);
        }
    }
}