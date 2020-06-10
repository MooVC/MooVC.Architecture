namespace MooVC.Architecture.Ddd.Services.Snapshots.SnapshotTests
{
    using System;
    using MooVC.Architecture.Ddd.EventCentricAggregateRootTests;
    using MooVC.Architecture.Ddd.Services.Reconciliation;
    using Xunit;

    public sealed class WhenSnapshotIsConstructed
    {
        [Fact]
        public void GivenAggregatesAndASequenceThenAnInstanceIsCreated()
        {
            EventCentricAggregateRoot[] aggregates = new[] { new SerializableEventCentricAggregateRoot() };
            var sequence = new EventSequence(2);
            var instance = new Snapshot(aggregates, sequence);

            Assert.Equal(aggregates, instance.Aggregates);
            Assert.Equal(sequence, instance.Sequence);
        }

        [Fact]
        public void GivenAggregatesAndANullSequenceThenAnArgumentNullExceptionIsThrown()
        {
            EventCentricAggregateRoot[] aggregates = new[] { new SerializableEventCentricAggregateRoot() };

            _ = Assert.Throws<ArgumentNullException>(() => new Snapshot(aggregates, default));
        }

        [Fact]
        public void GivenANullAggregatesCollectionAndASequenceThenAnInstanceIsCreated()
        {
            var sequence = new EventSequence(2);
            var instance = new Snapshot(default, sequence);

            Assert.Equal(new EventCentricAggregateRoot[0], instance.Aggregates);
            Assert.Equal(sequence, instance.Sequence);
        }

        [Fact]
        public void GivenNoAggregatesAndASequenceThenAnInstanceIsCreated()
        {
            var aggregates = new EventCentricAggregateRoot[0];
            var sequence = new EventSequence(2);
            var instance = new Snapshot(aggregates, sequence);

            Assert.Equal(aggregates, instance.Aggregates);
            Assert.Equal(sequence, instance.Sequence);
        }
    }
}