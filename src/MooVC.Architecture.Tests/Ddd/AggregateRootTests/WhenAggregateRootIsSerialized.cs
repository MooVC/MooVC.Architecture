namespace MooVC.Architecture.Ddd.AggregateRootTests
{
    using MooVC.Architecture.Serialization;
    using MooVC.Serialization;
    using Xunit;

    public sealed class WhenAggregateRootIsSerialized
    {
        [Fact]
        public void GivenAnInstanceThenAllPropertiesAreSerialized()
        {
            var original = new SerializableAggregateRoot();
            SerializableAggregateRoot deserialized = original.Clone();

            Assert.Equal(original, deserialized);
            Assert.NotSame(original, deserialized);

            Assert.Equal(original.Id, deserialized.Id);
            Assert.Equal(original.Version, deserialized.Version);
            Assert.Equal(original.GetHashCode(), deserialized.GetHashCode());
        }
    }
}