namespace MooVC.Architecture.Ddd.Services.DomainEventPropagatorTests
{
    using System;
    using MooVC.Architecture.Ddd.Services.Reconciliation;
    using Moq;
    using Xunit;

    public sealed class WhenDomainEventPropagatorIsConstructed
    {
        [Fact]
        public void GivenABusAndNoRepositoryThenAnArgumentNullExceptionIsThrown()
        {
            var bus = new Mock<IBus>();

            _ = Assert.Throws<ArgumentNullException>(
                () => new DomainEventPropagator<EventCentricAggregateRoot>(bus.Object, default!));
        }

        [Fact]
        public void GivenABusAndARepositoryThenNoExceptionIsThrown()
        {
            var bus = new Mock<IBus>();
            var repository = new Mock<IRepository<EventCentricAggregateRoot>>();

            _ = new DomainEventPropagator<EventCentricAggregateRoot>(bus.Object, repository.Object);
        }

        [Fact]
        public void GivenNoBusAndARepositoryThenAnArgumentNullExceptionIsThrown()
        {
            var repository = new Mock<IRepository<EventCentricAggregateRoot>>();

            _ = Assert.Throws<ArgumentNullException>(
                () => new DomainEventPropagator<EventCentricAggregateRoot>(default!, repository.Object));
        }

        [Fact]
        public void GivenABusAndNoReconcilerThenAnArgumentNullExceptionIsThrown()
        {
            var bus = new Mock<IBus>();

            _ = Assert.Throws<ArgumentNullException>(
                () => new DomainEventPropagator(bus.Object, default!));
        }

        [Fact]
        public void GivenABusAndAReconcilerThenNoExceptionIsThrown()
        {
            var bus = new Mock<IBus>();
            var reconciler = new Mock<IAggregateReconciler>();

            _ = new DomainEventPropagator(bus.Object, reconciler.Object);
        }

        [Fact]
        public void GivenNoBusAndAReconcilerThenAnArgumentNullExceptionIsThrown()
        {
            var reconciler = new Mock<IAggregateReconciler>();

            _ = Assert.Throws<ArgumentNullException>(
                () => new DomainEventPropagator(default!, reconciler.Object));
        }
    }
}