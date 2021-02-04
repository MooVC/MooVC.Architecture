namespace MooVC.Architecture.Ddd.ReferenceTests
{
    using System;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Serialization;
    using Xunit;

    public sealed class WhenReferenceIsSerialized
    {
        [Fact]
        public void GivenAnInstanceThenAllPropertiesAreSerialized()
        {
            var expectedId = Guid.NewGuid();
            var original = new Reference<SerializableAggregateRoot>(expectedId);
            Reference<SerializableAggregateRoot> deserialized = original.Clone();

            Assert.Equal(original, deserialized);
            Assert.NotSame(original, deserialized);

            Assert.Equal(expectedId, deserialized.Id);
            Assert.Equal(original.GetHashCode(), deserialized.GetHashCode());
        }
    }
}