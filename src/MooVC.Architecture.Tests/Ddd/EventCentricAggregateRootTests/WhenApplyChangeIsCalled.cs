namespace MooVC.Architecture.Ddd.EventCentricAggregateRootTests
{
    using System;
    using MooVC.Architecture.MessageTests;
    using MooVC.Serialization;
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

        [Fact]

        public void GivenAFailureWhenTheAggregateIsNewThenTheFailedChangeIsRemovedAndTheAggregateOtherChangesRemain()
        {
            var context = new SerializableMessage();
            var aggregate = new SerializableEventCentricAggregateRoot(context);

            SignedVersion original = aggregate.Version.Clone();

            Assert.True(aggregate.HasUncommittedChanges);
            Assert.True(original.IsNew);

            var fail = new FailRequest(context);

            Assert.Equal(original, aggregate.Version);

            _ = Assert.Throws<InvalidOperationException>(() => aggregate.Fail(fail));

            Assert.True(aggregate.HasUncommittedChanges);
            Assert.Equal(original, aggregate.Version);
        }

        [Fact]

        public void GivenAFailureWhenTheAggregateWasPersistedThenTheFailedChangeIsRemovedAndTheAggregateRolledBack()
        {
            var value = Guid.NewGuid();
            var context = new SerializableMessage();
            var request = new SetRequest(context, value);
            var aggregate = new SerializableEventCentricAggregateRoot(context);
            const ulong ExpectedVersion = 1;

            aggregate.MarkChangesAsCommitted();

            SignedVersion original = aggregate.Version.Clone();

            Assert.False(aggregate.HasUncommittedChanges);
            Assert.Equal(ExpectedVersion, original.Number);

            var fail = new FailRequest(context);

            _ = Assert.Throws<InvalidOperationException>(() => aggregate.Fail(fail));

            Assert.False(aggregate.HasUncommittedChanges);
            Assert.Equal(original, aggregate.Version);
        }

        [Fact]

        public void GivenAFailureWhenTheAggregateWasPersistedAndOtherChangesArePresentThenTheFailedChangeIsRemovedButTheAggregateIsNotRolledBack()
        {
            var value = Guid.NewGuid();
            var context = new SerializableMessage();
            var set = new SetRequest(context, value);
            var aggregate = new SerializableEventCentricAggregateRoot(context);
            const ulong ExpectedVersion = 2;

            aggregate.MarkChangesAsCommitted();

            aggregate.Set(set);

            SignedVersion original = aggregate.Version.Clone();

            Assert.True(aggregate.HasUncommittedChanges);
            Assert.Equal(ExpectedVersion, original.Number);

            var fail = new FailRequest(context);

            _ = Assert.Throws<InvalidOperationException>(() => aggregate.Fail(fail));

            Assert.True(aggregate.HasUncommittedChanges);
            Assert.Equal(original, aggregate.Version);
        }
    }
}