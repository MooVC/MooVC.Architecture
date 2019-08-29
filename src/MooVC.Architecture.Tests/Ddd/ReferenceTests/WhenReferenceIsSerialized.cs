namespace MooVC.Architecture.Ddd.ReferenceTests
{
    using System;
    using MooVC.Serialization;
    using Xunit;

    public sealed class WhenReferenceIsSerialized
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(18446744073709551615)]
        public void GivenAnInstanceThenAllPropertiesAreSerialized(ulong expectedVersion)
        {
            var expectedId = Guid.NewGuid();
            var reference = new Reference<AggregateRoot>(expectedId, version: expectedVersion);
            Reference<AggregateRoot> clone = reference.Clone();

            Assert.Equal(reference, clone);
            Assert.NotSame(reference, clone);

            Assert.Equal(expectedId, clone.Id);
            Assert.Equal(expectedVersion, clone.Version);
            Assert.Equal(reference.GetHashCode(), clone.GetHashCode());
        }
    }
}