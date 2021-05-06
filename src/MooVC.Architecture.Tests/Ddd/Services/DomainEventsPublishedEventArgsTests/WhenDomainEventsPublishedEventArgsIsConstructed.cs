namespace MooVC.Architecture.Ddd.Services.DomainEventsPublishedEventArgsTests
{
    using System.Collections.Generic;
    using MooVC.Architecture.Ddd.Services.DomainEventsEventArgsTests;
    using Xunit;

    public sealed class WhenDomainEventsPublishedEventArgsIsConstructed
        : DomainEventsEventArgsBase
    {
        [Fact]
        public void GivenNullEventsThenAnInstanceIsCreated()
        {
            var @event = new DomainEventsPublishedEventArgs(default!);

            Assert.Empty(@event.Events);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        public void GivenEventsThenAnInstanceIsCreated(int count)
        {
            IEnumerable<DomainEvent> events = CreateEvents(count);
            var @event = new DomainEventsPublishedEventArgs(events);

            Assert.NotSame(events, @event.Events);
        }
    }
}