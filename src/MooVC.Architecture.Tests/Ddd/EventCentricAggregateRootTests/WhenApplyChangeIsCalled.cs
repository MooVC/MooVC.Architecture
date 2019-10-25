namespace MooVC.Architecture.Ddd.EventCentricAggregateRootTests
{
    using System;
    using MooVC.Architecture.MessageTests;
    using Xunit;

    public sealed class WhenApplyChangeIsCalled
    {
        [Fact]
        public void GivenAValueThenTheValueIsPropagated()
        {
            var expectedValue = Guid.NewGuid();
            var context = new SerializableMessage();
            var request = new SetRequest(context, expectedValue);
            var aggregate = new SerializableEventCentricAggregateRoot(context);

            Assert.NotEqual(expectedValue, aggregate.Value);

            aggregate.Set(request);

            Assert.Equal(expectedValue, aggregate.Value);
        }

        [Fact]
        public void GivenAValueWhenTheAggregateIsNewThenTheVersionRemainsUnchanged()
        {
            var value = Guid.NewGuid();
            var context = new SerializableMessage();
            var request = new SetRequest(context, value);
            var aggregate = new SerializableEventCentricAggregateRoot(context);

            Assert.Equal(AggregateRoot.DefaultVersion, aggregate.Version);

            aggregate.Set(request);

            Assert.Equal(AggregateRoot.DefaultVersion, aggregate.Version);
        }

        [Fact]
        public void GivenAValueWhenTheAggregateIsNotNewThenTheVersionIsIncremented()
        {
            const ulong ExpectedVersion = 2;

            var value = Guid.NewGuid();
            var context = new SerializableMessage();
            var request = new SetRequest(context, value);
            var aggregate = new SerializableEventCentricAggregateRoot(context);

            aggregate.MarkChangesAsCommitted();

            Assert.Equal(AggregateRoot.DefaultVersion, aggregate.Version);

            aggregate.Set(request);

            Assert.Equal(ExpectedVersion, aggregate.Version);
        }
    }
}