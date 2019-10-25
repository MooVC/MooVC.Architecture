namespace MooVC.Architecture.Ddd.AggregateRootTests
{
    using System;
    using MooVC.Architecture.Ddd.DomainEventTests;
    using MooVC.Architecture.MessageTests;
    using MooVC.Serialization;
    using Xunit;

    public sealed class WhenDomainEventIsSerialized
    {
        [Theory]
        [InlineData(1)]
        [InlineData(18446744073709551615)]
        public void GivenAnInstanceThenAllPropertiesAreSerialized(ulong expectedVersion)
        {
            var expectedId = Guid.NewGuid();
            var expectedAggregate = new VersionedReference<AggregateRoot>(expectedId, version: expectedVersion);
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