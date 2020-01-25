namespace MooVC.Architecture.Ddd.AggregateRootExtensionsTests
{
    using System;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using Xunit;

    public sealed class WhenToVersionedReferenceIsCalled
    {
        [Fact]
        public void GivenAnAggregateThenAReferenceWithTheSameIdTypeAndVersionIsReturned()
        {
            var aggregateId = Guid.NewGuid();
            var aggregate = new SerializableAggregateRoot(aggregateId);

            var reference = aggregate.ToVersionedReference();

            Assert.Equal(aggregateId, reference.Id);
            Assert.Equal(typeof(SerializableAggregateRoot), reference.Type);
            Assert.Equal(aggregate.Version, reference.Version);
        }
    }
}