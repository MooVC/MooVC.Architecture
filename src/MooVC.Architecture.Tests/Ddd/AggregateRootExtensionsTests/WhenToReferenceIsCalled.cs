namespace MooVC.Architecture.Ddd.AggregateRootExtensionsTests
{
    using System;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using Moq;
    using Xunit;

    public sealed class WhenToReferenceIsCalled
    {
        [Fact]
        public void GivenAnAggregateThenAReferenceWithTheSameIdAndTypeIsReturned()
        {
            var aggregateId = Guid.NewGuid();
            var aggregate = new SerializableAggregateRoot(aggregateId);

            var reference = aggregate.ToReference();

            Assert.Equal(aggregateId, reference.Id);
            Assert.Equal(typeof(SerializableAggregateRoot), reference.Type);
        }
    }
}