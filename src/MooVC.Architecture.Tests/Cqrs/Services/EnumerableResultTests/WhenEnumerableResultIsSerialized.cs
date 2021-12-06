namespace MooVC.Architecture.Cqrs.Services.EnumerableResultTests
{
    using MooVC.Architecture.MessageTests;
    using MooVC.Architecture.Serialization;
    using Xunit;

    public sealed class WhenEnumerableResultIsSerialized
    {
        [Fact]
        public void GivenAnInstanceThenAllPropertiesAreSerialized()
        {
            var context = new SerializableMessage();
            var result = new SerializableEnumerableResult<int>(context, new[] { 1, 2, 3 });
            SerializableEnumerableResult<int> deserialized = result.Clone();

            Assert.Equal(result, deserialized);
            Assert.NotSame(result, deserialized);
            Assert.Equal(result.Value, deserialized.Value);
            Assert.Equal(result.GetHashCode(), deserialized.GetHashCode());
        }
    }
}