namespace MooVC.Architecture.Ddd.DomainEventTests
{
    using MooVC.Architecture.MessageTests;
    using Xunit;

    public sealed class WhenImplicityCastToReference
    {
        [Fact]
        public void GivenAnEventWhenCastToATypedReferenceThenTheTypedReferenceIsReturned()
        {
            var aggregate = new SerializableEventCentricAggregateRoot();
            var message = new SerializableMessage();
            var @event = new SerializableDomainEvent<SerializableEventCentricAggregateRoot>(message, aggregate);
            Reference<SerializableEventCentricAggregateRoot> reference = @event;

            Assert.True(reference.IsMatch(aggregate));
        }

        [Fact]
        public void GivenAnEventWhenCastToAbUntypedReferenceThenTheTypedReferenceIsReturned()
        {
            var aggregate = new SerializableEventCentricAggregateRoot();
            var message = new SerializableMessage();
            var @event = new SerializableDomainEvent<SerializableEventCentricAggregateRoot>(message, aggregate);
            Reference reference = @event;

            Assert.True(reference.IsMatch(aggregate));
        }
    }
}