namespace MooVC.Architecture.Ddd.Services.PersistentBusTests
{
    using System;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Architecture.Ddd.DomainEventTests;
    using MooVC.Architecture.Ddd.Services;
    using MooVC.Architecture.MessageTests;
    using MooVC.Persistence;
    using Moq;
    using Xunit;

    public sealed class WhenPublishIsCalled
    {
        private readonly SerializableAggregateRoot aggregate;
        private readonly PersistentBus bus;
        private readonly SerializableMessage context;
        private readonly Mock<IStore<AtomicUnit, Guid>> store;
        private int invocationCounter = 0;

        public WhenPublishIsCalled()
        {
            aggregate = new SerializableAggregateRoot();
            context = new SerializableMessage();
            store = new Mock<IStore<AtomicUnit, Guid>>();
            bus = new PersistentBus(store.Object);
        }

        [Fact]
        public void GivenAFailureOnPublishThenUnhandledIsRaisedOnce()
        {
            GivenAFailureOnPublishThenEventIsRaisedOnce(() => bus.Unhandled += (sender, e) => invocationCounter++);
        }

        [Fact]
        public void GivenAFailureOnPublishThenPublishedIsRaisedOnce()
        {
            GivenAFailureOnPublishThenEventIsRaisedOnce(() => bus.Published += (sender, e) => invocationCounter++);
        }

        [Fact]
        public void GivenMultipleEventsThenCreateIsCalledOnce()
        {
            GivenOneOrMoreEventsThenCreateIsCalledOnce(
                new SerializableDomainEvent<SerializableAggregateRoot>(context, aggregate),
                new SerializableDomainEvent<SerializableAggregateRoot>(context, aggregate));
        }

        [Fact]
        public void GivenAFailureOnPublishThenPublishingIsRaisedOnce()
        {
            GivenAFailureOnPublishThenEventIsRaisedOnce(() => bus.Publishing += (sender, e) => invocationCounter++);
        }

        [Fact]
        public void GivenMultipleEventsThenPublishedIsRaisedOnce()
        {
            GivenMultipleEventsThenEventIsRaisedOnce(() => bus.Published += (sender, e) => invocationCounter++);
        }

        [Fact]
        public void GivenMultipleEventsThenPublishingIsRaisedOnce()
        {
            GivenMultipleEventsThenEventIsRaisedOnce(() => bus.Publishing += (sender, e) => invocationCounter++);
        }

        [Fact]
        public void GivenNoEventsThenNothingHappens()
        {
            bool wasInvoked = false;

            bus.Published += (sender, e) => wasInvoked = true;
            bus.Publishing += (sender, e) => wasInvoked = true;

            bus.Publish();

            Assert.False(wasInvoked);
        }

        [Fact]
        public void GivenOneEventThenCreateIsCalledOnce()
        {
            GivenOneOrMoreEventsThenCreateIsCalledOnce(
                new SerializableDomainEvent<SerializableAggregateRoot>(context, aggregate));
        }

        [Fact]
        public void GivenOneEventThenPublishedIsRaisedOnce()
        {
            GivenOneEventThenEventIsRaisedOnce(() => bus.Published += (sender, e) => invocationCounter++);
        }

        [Fact]
        public void GivenOneEventThenPublishingIsRaisedOnce()
        {
            GivenOneEventThenEventIsRaisedOnce(() => bus.Publishing += (sender, e) => invocationCounter++);
        }

        private void GivenAFailureOnPublishThenEventIsRaisedOnce(Action @event)
        {
            const int ExpectedInvocationCount = 1;

            @event();

            _ = store
                .Setup(store => store.CreateAsync(It.IsAny<AtomicUnit>()))
                .Throws<InvalidOperationException>();

            bus.Publish(new SerializableDomainEvent<SerializableAggregateRoot>(context, aggregate));

            Assert.Equal(ExpectedInvocationCount, invocationCounter);
        }

        private void GivenMultipleEventsThenEventIsRaisedOnce(Action @event)
        {
            const int ExpectedInvocationCount = 1;

            @event();

            bus.Publish(
                new SerializableDomainEvent<SerializableAggregateRoot>(context, aggregate),
                new SerializableDomainEvent<SerializableAggregateRoot>(context, aggregate));

            Assert.Equal(ExpectedInvocationCount, invocationCounter);
        }

        private void GivenOneEventThenEventIsRaisedOnce(Action @event)
        {
            const int ExpectedInvocationCount = 1;

            @event();

            bus.Publish(new SerializableDomainEvent<SerializableAggregateRoot>(context, aggregate));

            Assert.Equal(ExpectedInvocationCount, invocationCounter);
        }

        private void GivenOneOrMoreEventsThenCreateIsCalledOnce(params SerializableDomainEvent<SerializableAggregateRoot>[] events)
        {
            const int ExpectedInvocationCount = 1;

            _ = store
                .Setup(store => store.CreateAsync(It.IsAny<AtomicUnit>()))
                .Callback(() => invocationCounter++);

            bus.Publish(events);

            Assert.Equal(ExpectedInvocationCount, invocationCounter);
        }
    }
}