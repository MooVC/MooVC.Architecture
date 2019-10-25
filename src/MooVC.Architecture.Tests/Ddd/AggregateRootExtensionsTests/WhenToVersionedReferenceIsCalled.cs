namespace MooVC.Architecture.Ddd.AggregateRootExtensionsTests
{
    using System;
    using Moq;
    using Xunit;

    public sealed class WhenToVersionedReferenceIsCalled
    {
        [Theory]
        [InlineData(1ul)]
        [InlineData(18446744073709551615)]
        public void GivenAnAggregateThenAReferenceWithTheSameIdTypeAndVersionIsReturned(ulong expectedVersion)
        {
            var aggregateId = Guid.NewGuid();
            var aggregate = new Mock<AggregateRoot>(aggregateId, expectedVersion);

            var reference = aggregate.Object.ToVersionedReference();

            Assert.Equal(aggregateId, reference.Id);
            Assert.Equal(typeof(AggregateRoot), reference.Type);
            Assert.Equal(expectedVersion, reference.Version);
        }
    }
}