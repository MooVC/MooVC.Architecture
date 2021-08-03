namespace MooVC.Architecture.Ddd.AggregateHistoryInvalidForStateExceptionTests
{
    using System.Collections.Generic;
    using System.Linq;
    using MooVC.Architecture.Ddd.EventCentricAggregateRootTests;
    using MooVC.Architecture.MessageTests;
    using MooVC.Architecture.Serialization;
    using Xunit;

    public sealed class WhenAggregateHistoryInvalidForStateExceptionIsSerialized
    {
        [Fact]
        public void GivenAnInstanceThenAllPropertiesAreSerialized()
        {
            var first = new SerializableEventCentricAggregateRoot();
            var second = new SerializableEventCentricAggregateRoot(first.Id);
            var context = new SerializableMessage();

            IEnumerable<DomainEvent> events = first.ApplyChanges(context, times: 5);

            second.LoadFromHistory(events.Take(2));

            AggregateHistoryInvalidForStateException original = Assert.Throws<AggregateHistoryInvalidForStateException>(
                () => second.LoadFromHistory(events.Skip(3)));

            AggregateHistoryInvalidForStateException deserialized = original.Clone();

            Assert.NotSame(original, deserialized);
            Assert.Equal(original.Aggregate, deserialized.Aggregate);
            Assert.Equal(original.Events, deserialized.Events);
            Assert.Equal(original.StartingVersion, deserialized.StartingVersion);
        }
    }
}