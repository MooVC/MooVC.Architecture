namespace MooVC.Architecture.Ddd.EventCentricAggregateRootTests
{
    using System;
    using MooVC.Architecture.MessageTests;
    using MooVC.Architecture.Serialization;
    using Xunit;

    public sealed class WhenEventCentricAggregateRootIsSerialized
    {
        [Fact]
        public void GivenAnInstanceWhenNoChangesArePendingThenAllPropertiesAreSerialized()
        {
            var expectedId = Guid.NewGuid();
            var original = new SerializableEventCentricAggregateRoot(expectedId);

            original.MarkChangesAsCommitted();

            SerializableEventCentricAggregateRoot deserialized = original.Clone();

            Assert.Equal(original, deserialized);
            Assert.NotSame(original, deserialized);

            Assert.Empty(deserialized.GetUncommittedChanges());
            Assert.Equal(expectedId, deserialized.Id);
            Assert.Equal(original.GetHashCode(), deserialized.GetHashCode());
        }

        [Fact]
        public void GivenAnInstanceWhenChangesArePendingThenAllPropertiesAreSerialized()
        {
            var expectedId = Guid.NewGuid();
            var value = Guid.NewGuid();
            var context = new SerializableMessage();
            var request = new SetRequest(context, value);
            var original = new SerializableEventCentricAggregateRoot(expectedId);

            original.Set(request);

            SerializableEventCentricAggregateRoot deserialized = original.Clone();

            Assert.Equal(original, deserialized);
            Assert.NotSame(original, deserialized);

            Assert.Equal(original.GetUncommittedChanges(), deserialized.GetUncommittedChanges());
            Assert.Equal(expectedId, deserialized.Id);
            Assert.Equal(original.GetHashCode(), deserialized.GetHashCode());
        }
    }
}