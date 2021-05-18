namespace MooVC.Architecture.Ddd.Services.DomainEventsPublishedAsyncEventArgsTests
{
    using System.Collections.Generic;
    using MooVC.Architecture.Ddd.Services.DomainEventsAsyncEventArgsTests;
    using Xunit;

    public sealed class WhenDomainEventsPublishedAsyncEventArgsIsConstructed
        : DomainEventsAsyncEventArgsBase
    {
        [Fact]
        public void GivenNullEventsThenAnInstanceIsCreated()
        {
            var @event = new DomainEventsPublishedAsyncEventArgs(default!);

            Assert.Empty(@event.Events);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        public void GivenEventsThenAnInstanceIsCreated(int count)
        {
            IEnumerable<DomainEvent> events = CreateEvents(count);
            var @event = new DomainEventsPublishedAsyncEventArgs(events);

            Assert.NotSame(events, @event.Events);
        }
    }
}