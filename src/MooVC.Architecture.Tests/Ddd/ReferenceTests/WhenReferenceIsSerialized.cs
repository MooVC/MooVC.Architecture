namespace MooVC.Architecture.Ddd.ReferenceTests
{
    using System;
    using MooVC.Serialization;
    using Xunit;

    public sealed class WhenReferenceIsSerialized
    {
        [Fact]
        public void GivenAnInstanceThenAllPropertiesAreSerialized()
        {
            var expectedId = Guid.NewGuid();
            var reference = new Reference<AggregateRoot>(expectedId);
            Reference<AggregateRoot> clone = reference.Clone();

            Assert.Equal(reference, clone);
            Assert.NotSame(reference, clone);

            Assert.Equal(expectedId, clone.Id);
            Assert.Equal(reference.GetHashCode(), clone.GetHashCode());
        }
    }
}