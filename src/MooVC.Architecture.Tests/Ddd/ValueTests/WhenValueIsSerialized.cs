namespace MooVC.Architecture.Ddd.ValueTests
{
    using MooVC.Serialization;
    using Xunit;

    public sealed class WhenValueIsSerialized
    {
        [Theory]
        [InlineData(0, "", true)]
        [InlineData(2147483647, "Test 1", true)]
        [InlineData(-2147483648, "Test 2", true)]
        [InlineData(0, null, true)]
        [InlineData(0, "", false)]
        public void GivenAnInstanceThenAllPropertiesAreSerialized(int expectedFirst, string expectedSecond, bool setThird)
        {
            SerializableValue expectedThird = setThird 
                ? new SerializableValue() 
                : default;

            var value = new SerializableValue(first: expectedFirst, second: expectedSecond, third: expectedThird);
            SerializableValue clone = value.Clone();

            Assert.Equal(value, clone);
            Assert.NotSame(value, clone);

            Assert.Equal(expectedFirst, clone.First);
            Assert.Equal(expectedSecond, clone.Second);
            Assert.Equal(expectedThird, clone.Third);
            Assert.Equal(value.GetHashCode(), clone.GetHashCode());
        }
    }
}