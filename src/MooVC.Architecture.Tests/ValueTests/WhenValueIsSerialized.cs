namespace MooVC.Architecture.ValueTests
{
    using System.Collections.Generic;
    using MooVC.Architecture.Serialization;
    using Xunit;

    public sealed class WhenValueIsSerialized
    {
        [Theory]
        [InlineData(0, "", true, new[] { "One", "Two" })]
        [InlineData(2147483647, "Test 1", true, new[] { "One", "Two", "Three", "Four" })]
        [InlineData(-2147483648, "Test 2", true, new string[0])]
        [InlineData(0, default, true, new[] { "One", "Two", "Three" })]
        [InlineData(0, "", false, new[] { "One" })]
        public void GivenAnInstanceThenAllPropertiesAreSerialized(
            int expectedFirst,
            string? expectedSecond,
            bool setThird,
            IEnumerable<string>? expectedFourth)
        {
            SerializableValue? expectedThird = setThird
                ? new SerializableValue()
                : default;

            var value = new SerializableValue(
                first: expectedFirst,
                second: expectedSecond,
                third: expectedThird,
                fourth: expectedFourth);

            SerializableValue deserialized = value.Clone();

            Assert.Equal(value, deserialized);
            Assert.NotSame(value, deserialized);

            Assert.Equal(expectedFirst, deserialized.First);
            Assert.Equal(expectedSecond, deserialized.Second);
            Assert.Equal(expectedThird, deserialized.Third);
            Assert.Equal(expectedFourth, deserialized.Fourth);
            Assert.Equal(value.GetHashCode(), deserialized.GetHashCode());
        }
    }
}