namespace MooVC.Architecture.Ddd.ProjectionTests
{
    using MooVC.Architecture.Ddd.EventCentricAggregateRootTests;
    using MooVC.Architecture.Serialization;
    using Xunit;

    public sealed class WhenProjectionIsSerialized
    {
        [Fact]
        public void GivenAnInstanceThenAllPropertiesAreSerialized()
        {
            var aggregate = new SerializableEventCentricAggregateRoot();
            var original = new SerializableProjection<SerializableEventCentricAggregateRoot>(aggregate);
            SerializableProjection<SerializableEventCentricAggregateRoot> deserialized = original.Clone();

            Assert.NotSame(original, deserialized);
            Assert.Equal(original.Aggregate, deserialized.Aggregate);
        }
    }
}