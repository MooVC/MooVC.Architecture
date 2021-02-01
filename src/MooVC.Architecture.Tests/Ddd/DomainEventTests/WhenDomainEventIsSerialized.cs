namespace MooVC.Architecture.Ddd.AggregateRootTests
{
    using MooVC.Architecture.Ddd.DomainEventTests;
    using MooVC.Architecture.MessageTests;
    using MooVC.Architecture.Serialization;
    using Xunit;

    public sealed class WhenDomainEventIsSerialized
    {
        [Fact]
        public void GivenAnInstanceThenAllPropertiesAreSerialized()
        {
            var aggregate = new SerializableAggregateRoot();
            var expectedContext = new SerializableMessage();
            var @event = new SerializableDomainEvent<SerializableAggregateRoot>(expectedContext, aggregate);
            SerializableDomainEvent<SerializableAggregateRoot> clone = @event.Clone();

            Assert.Equal(@event, clone);
            Assert.NotSame(@event, clone);

            Assert.Equal(@event.Aggregate, clone.Aggregate);
            Assert.Equal(@event.CausationId, clone.CausationId);
            Assert.Equal(@event.CorrelationId, clone.CorrelationId);
            Assert.Equal(@event.Id, clone.Id);
            Assert.Equal(@event.TimeStamp, clone.TimeStamp);
            Assert.Equal(@event.GetHashCode(), clone.GetHashCode());
        }
    }
}