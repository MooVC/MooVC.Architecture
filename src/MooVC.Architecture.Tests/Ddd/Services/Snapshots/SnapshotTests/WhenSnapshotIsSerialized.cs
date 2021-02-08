namespace MooVC.Architecture.Ddd.Services.Snapshots.SnapshotTests
{
    using MooVC.Architecture.Ddd.EventCentricAggregateRootTests;
    using MooVC.Architecture.Ddd.Services.Reconciliation;
    using MooVC.Architecture.Serialization;
    using MooVC.Serialization;
    using Xunit;

    public sealed class WhenSnapshotIsSerialized
    {
        [Fact]
        public void GivenAnInstanceThenAllPropertiesAreSerialized()
        {
            EventCentricAggregateRoot[] aggregates = new[] { new SerializableEventCentricAggregateRoot() };
            var sequence = new EventSequence(2);
            var instance = new Snapshot(aggregates, sequence);
            Snapshot deserialized = instance.Clone();

            Assert.Equal(instance.Aggregates, deserialized.Aggregates);
            Assert.Equal(instance.Sequence.Sequence, deserialized.Sequence.Sequence);
            Assert.Equal(instance.Sequence.TimeStamp, deserialized.Sequence.TimeStamp);
            Assert.NotSame(instance, deserialized);
        }
    }
}