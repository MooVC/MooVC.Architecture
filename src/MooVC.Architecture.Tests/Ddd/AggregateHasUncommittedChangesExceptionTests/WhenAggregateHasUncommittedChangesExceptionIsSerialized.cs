namespace MooVC.Architecture.Ddd.AggregateHasUncommittedChangesExceptionTests
{
    using System.Collections.Generic;
    using MooVC.Architecture.Ddd.EventCentricAggregateRootTests;
    using MooVC.Architecture.MessageTests;
    using MooVC.Architecture.Serialization;
    using Xunit;

    public sealed class WhenAggregateHasUncommittedChangesExceptionIsSerialized
    {
        [Fact]
        public void GivenAnInstanceThenAllPropertiesAreSerialized()
        {
            var aggregate = new SerializableEventCentricAggregateRoot();
            var context = new SerializableMessage();

            IEnumerable<DomainEvent> events = aggregate.ApplyChanges(context, commit: false, times: 1);

            AggregateHasUncommittedChangesException original = Assert.Throws<AggregateHasUncommittedChangesException>(
                () => aggregate.LoadFromHistory(events));

            AggregateHasUncommittedChangesException deserialized = original.Clone();

            Assert.NotSame(original, deserialized);
            Assert.Equal(original.Aggregate, deserialized.Aggregate);
        }
    }
}