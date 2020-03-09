namespace MooVC.Architecture.Ddd.ValueTests
{
    using MooVC.Serialization;
    using Xunit;

    public sealed class WhenValueIsSerialized
    {
        [Theory]
        [InlineData(0, "", true, new[] { "One", "Two" })]
        [InlineData(2147483647, "Test 1", true, new[] { "One", "Two", "Three", "Four" })]
        [InlineData(-2147483648, "Test 2", true, new string[0])]
        [InlineData(0, null, true, new[] { "One", "Two", "Three" })]
        [InlineData(0, "", false, new[] { "One" })]
        public void GivenAnInstanceThenAllPropertiesAreSerialized(int expectedFirst, string expectedSecond, bool setThird, string[] expectedFourth)
        {
            SerializableValue expectedThird = setThird
                ? new SerializableValue()
                : default;

            var value = new SerializableValue(
                first: expectedFirst,
                second: expectedSecond,
                third: expectedThird,
                fourth: expectedFourth);
            SerializableValue clone = value.Clone();

            Assert.Equal(value, clone);
            Assert.NotSame(value, clone);

            Assert.Equal(expectedFirst, clone.First);
            Assert.Equal(expectedSecond, clone.Second);
            Assert.Equal(expectedThird, clone.Third);
            Assert.Equal(expectedFourth, clone.Fourth);
            Assert.Equal(value.GetHashCode(), clone.GetHashCode());
        }
    }
}