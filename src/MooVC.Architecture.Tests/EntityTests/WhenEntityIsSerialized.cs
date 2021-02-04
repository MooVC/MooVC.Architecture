namespace MooVC.Architecture.EntityTests
{
    using MooVC.Architecture.Serialization;
    using MooVC.Serialization;
    using Xunit;

    public sealed class WhenEntityIsSerialized
    {
        [Fact]
        public void GivenAnInstanceThenAllPropertiesAreSerialized()
        {
            var original = new SerializableEntity<int>(1);
            SerializableEntity<int> deserialized = original.Clone();

            Assert.Equal(original, deserialized);
            Assert.NotSame(original, deserialized);
            Assert.Equal(original.Id, deserialized.Id);
        }
    }
}