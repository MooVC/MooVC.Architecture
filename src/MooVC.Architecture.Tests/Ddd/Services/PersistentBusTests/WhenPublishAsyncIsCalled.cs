namespace MooVC.Architecture.Ddd.Services.PersistentBusTests
{
    using System;
    using System.Threading.Tasks;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Architecture.Ddd.DomainEventTests;
    using MooVC.Architecture.Ddd.Services;
    using MooVC.Architecture.MessageTests;
    using MooVC.Persistence;
    using Moq;
    using Xunit;

    public sealed class WhenPublishAsyncIsCalled
    {
        private readonly SerializableAggregateRoot aggregate;
        private readonly PersistentBus bus;
        private readonly SerializableMessage context;
        private readonly Mock<IStore<AtomicUnit, Guid>> store;
        private int invocationCounter = 0;

        public WhenPublishAsyncIsCalled()
        {
            aggregate = new SerializableAggregateRoot();
            context = new SerializableMessage();
            store = new Mock<IStore<AtomicUnit, Guid>>();
            bus = new PersistentBus(store.Object);
        }

        [Fact]
        public async Task GivenAFailureOnPublishThenTheFailureIsThrownAsync()
        {
            _ = store
                .Setup(store => store.CreateAsync(It.IsAny<AtomicUnit>()))
                .Throws<InvalidOperationException>();

            _ = await Assert.ThrowsAsync<InvalidOperationException>(() => bus.PublishAsync(
                new SerializableDomainEvent<SerializableAggregateRoot>(context, aggregate)));
        }

        [Fact]
        public async Task GivenMultipleEventsThenCreateIsCalledOnceAsync()
        {
            await GivenOneOrMoreEventsThenCreateIsCalledOnceAsync(
                new SerializableDomainEvent<SerializableAggregateRoot>(context, aggregate),
                new SerializableDomainEvent<SerializableAggregateRoot>(context, aggregate));
        }

        [Fact]
        public async Task GivenMultipleEventsThenPublishedIsRaisedOnceAsync()
        {
            await GivenMultipleEventsThenEventIsRaisedOnceAsync(
                () => bus.Published += (sender, e) => Task.FromResult(invocationCounter++));
        }

        [Fact]
        public async Task GivenMultipleEventsThenPublishingIsRaisedOnceAsync()
        {
            await GivenMultipleEventsThenEventIsRaisedOnceAsync(
                () => bus.Publishing += (sender, e) => Task.FromResult(invocationCounter++));
        }

        [Fact]
        public async Task GivenNoEventsThenNothingHappensAsync()
        {
            bool wasInvoked = false;

            bus.Published += (sender, e) => Task.FromResult(wasInvoked = true);
            bus.Publishing += (sender, e) => Task.FromResult(wasInvoked = true);

            await bus.PublishAsync();

            Assert.False(wasInvoked);
        }

        [Fact]
        public async Task GivenOneEventThenCreateIsCalledOnceAsync()
        {
            await GivenOneOrMoreEventsThenCreateIsCalledOnceAsync(
                new SerializableDomainEvent<SerializableAggregateRoot>(context, aggregate));
        }

        [Fact]
        public async Task GivenOneEventThenPublishedIsRaisedOnceAsync()
        {
            await GivenOneEventThenEventIsRaisedOnceAsync(
                () => bus.Published += (sender, e) => Task.FromResult(invocationCounter++));
        }

        [Fact]
        public async Task GivenOneEventThenPublishingIsRaisedOnceAsync()
        {
            await GivenOneEventThenEventIsRaisedOnceAsync(
                () => bus.Publishing += (sender, e) => Task.FromResult(invocationCounter++));
        }

        private async Task GivenMultipleEventsThenEventIsRaisedOnceAsync(Action @event)
        {
            const int ExpectedInvocationCount = 1;

            @event();

            await bus.PublishAsync(
                new SerializableDomainEvent<SerializableAggregateRoot>(context, aggregate),
                new SerializableDomainEvent<SerializableAggregateRoot>(context, aggregate));

            Assert.Equal(ExpectedInvocationCount, invocationCounter);
        }

        private async Task GivenOneEventThenEventIsRaisedOnceAsync(Action @event)
        {
            const int ExpectedInvocationCount = 1;

            @event();

            await bus.PublishAsync(
                new SerializableDomainEvent<SerializableAggregateRoot>(context, aggregate));

            Assert.Equal(ExpectedInvocationCount, invocationCounter);
        }

        private async Task GivenOneOrMoreEventsThenCreateIsCalledOnceAsync(
            params SerializableDomainEvent<SerializableAggregateRoot>[] events)
        {
            const int ExpectedInvocationCount = 1;

            _ = store
                .Setup(store => store.CreateAsync(It.IsAny<AtomicUnit>()))
                .Callback(() => invocationCounter++);

            await bus.PublishAsync(events);

            Assert.Equal(ExpectedInvocationCount, invocationCounter);
        }
    }
}