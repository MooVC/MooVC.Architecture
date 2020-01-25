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
            SignedVersion version = aggregate.Version;

            Assert.True(version.IsNew);

            aggregate.Set(request);

            Assert.Equal(version, aggregate.Version);
        }

        [Fact]
        public void GivenAValueWhenTheAggregateIsNotNewThenTheVersionIsIncremented()
        {
            var value = Guid.NewGuid();
            var context = new SerializableMessage();
            var request = new SetRequest(context, value);
            var aggregate = new SerializableEventCentricAggregateRoot(context);

            aggregate.MarkChangesAsCommitted();

            SignedVersion version = aggregate.Version;

            Assert.True(version.IsNew);

            aggregate.Set(request);

            Assert.NotEqual(version, aggregate.Version);
        }
    }
}