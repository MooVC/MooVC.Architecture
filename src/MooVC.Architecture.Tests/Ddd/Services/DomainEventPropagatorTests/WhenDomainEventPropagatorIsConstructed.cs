namespace MooVC.Architecture.Ddd.Services.DomainEventPropagatorTests
{
    using System;
    using Moq;
    using Xunit;

    public sealed class WhenDomainEventPropagatorIsConstructed
    {
        [Fact]
        public void GivenABusAndNoRepositoryThenAnArgumentNullExceptionIsThrown()
        {
            var bus = new Mock<IBus>();

            _ = Assert.Throws<ArgumentNullException>(
                () => new DomainEventPropagator<EventCentricAggregateRoot>(bus.Object, null));
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
                () => new DomainEventPropagator<EventCentricAggregateRoot>(null, repository.Object));
        }
    }
}