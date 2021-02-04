namespace MooVC.Architecture.Ddd.EntityTests
{
    using System;
    using MooVC.Architecture.Serialization;
    using MooVC.Serialization;
    using Xunit;

    public sealed class WhenEntityIsSerialized
    {
        [Fact]
        public void GivenAnInstanceThenAllPropertiesAreSerialized()
        {
            var expectedId = Guid.NewGuid();
            var original = new SerializableEntity(expectedId);
            SerializableEntity deserialized = original.Clone();

            Assert.Equal(original, deserialized);
            Assert.NotSame(original, deserialized);

            Assert.Equal(expectedId, deserialized.Id);
            Assert.Equal(original.GetHashCode(), deserialized.GetHashCode());
        }
    }
}