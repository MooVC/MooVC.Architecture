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
            var entity = new SerializableEntity(expectedId);
            SerializableEntity clone = entity.Clone();

            Assert.Equal(entity, clone);
            Assert.NotSame(entity, clone);

            Assert.Equal(expectedId, clone.Id);
            Assert.Equal(entity.GetHashCode(), clone.GetHashCode());
        }
    }
}