namespace MooVC.Architecture.Ddd.Services.DomainEventsPublishingAsyncEventArgsTests
{
    using System.Collections.Generic;
    using MooVC.Architecture.Ddd.Services.DomainEventsAsyncEventArgsTests;
    using Xunit;

    public sealed class WhenDomainEventsPublishingAsyncEventArgsIsConstructed
        : DomainEventsAsyncEventArgsBase
    {
        [Fact]
        public void GivenNullEventsThenAnInstanceIsCreated()
        {
            var @event = new DomainEventsPublishingAsyncEventArgs(default!);

            Assert.Empty(@event.Events);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        public void GivenEventsThenAnInstanceIsCreated(int count)
        {
            IEnumerable<DomainEvent> events = CreateEvents(count);
            var @event = new DomainEventsPublishingAsyncEventArgs(events);

            Assert.NotSame(events, @event.Events);
        }
    }
}