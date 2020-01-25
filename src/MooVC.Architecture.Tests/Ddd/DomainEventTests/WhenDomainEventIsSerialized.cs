namespace MooVC.Architecture.Ddd.AggregateRootTests
{
    using System;
    using MooVC.Architecture.Ddd.DomainEventTests;
    using MooVC.Architecture.MessageTests;
    using MooVC.Serialization;
    using Xunit;

    public sealed class WhenDomainEventIsSerialized
    {
        [Fact]
        public void GivenAnInstanceThenAllPropertiesAreSerialized()
        {
            var aggregate = new SerializableAggregateRoot();
            var expectedAggregate = new VersionedReference<AggregateRoot>(aggregate);
            var expectedContext = new SerializableMessage();

            var @event = new SerializableDomainEvent(expectedContext, expectedAggregate);
            SerializableDomainEvent clone = @event.Clone();

            Assert.Equal(@event, clone);
            Assert.NotSame(@event, clone);

            Assert.Equal(@event.Aggregate, @event.Aggregate);
            Assert.Equal(@event.CausationId, @event.CausationId);
            Assert.Equal(@event.CorrelationId, @event.CorrelationId);
            Assert.Equal(@event.Id, @event.Id);
            Assert.Equal(@event.TimeStamp, @event.TimeStamp);
            Assert.Equal(@event.GetHashCode(), clone.GetHashCode());
        }
    }
}