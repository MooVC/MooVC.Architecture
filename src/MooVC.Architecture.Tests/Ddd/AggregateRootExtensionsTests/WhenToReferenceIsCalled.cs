namespace MooVC.Architecture.Ddd.AggregateRootExtensionsTests
{
    using System;
    using Moq;
    using Xunit;

    public sealed class WhenToReferenceIsCalled
    {
        [Fact]
        public void GivenAnAggregateThenAReferenceWithTheSameIdAndTypeIsReturned()
        {
            var aggregateId = Guid.NewGuid();
            var aggregate = new Mock<AggregateRoot>(aggregateId);

            var reference = aggregate.Object.ToReference();

            Assert.Equal(aggregateId, reference.Id);
            Assert.Equal(typeof(AggregateRoot), reference.Type);
        }
    }
}