namespace MooVC.Architecture.Ddd.AggregateEventSequenceUnorderedExceptionTests
{
    using System.Collections.Generic;
    using System.Linq;
    using MooVC.Architecture.Ddd.EventCentricAggregateRootTests;
    using MooVC.Architecture.MessageTests;
    using MooVC.Architecture.Serialization;
    using Xunit;

    public sealed class WhenAggregateEventSequenceUnorderedExceptionIsSerialized
    {
        [Fact]
        public void GivenAnInstanceThenAllPropertiesAreSerialized()
        {
            var aggregate = new SerializableEventCentricAggregateRoot();
            var context = new SerializableMessage();

            IEnumerable<DomainEvent> events = aggregate.ApplyChanges(context, times: 3);

            AggregateEventSequenceUnorderedException original = Assert.Throws<AggregateEventSequenceUnorderedException>(
                () => aggregate.LoadFromHistory(events.OrderByDescending(@event => @event.Aggregate.Version)));

            AggregateEventSequenceUnorderedException deserialized = original.Clone();

            Assert.NotSame(original, deserialized);
            Assert.Equal(original.Aggregate, deserialized.Aggregate);
            Assert.Equal(original.Events, deserialized.Events);
        }
    }
}