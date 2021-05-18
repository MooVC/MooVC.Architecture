namespace MooVC.Architecture.Ddd.Services.DomainEventsPublishedEventArgsTests
{
    using System.Collections.Generic;
    using MooVC.Architecture.Ddd.Services.DomainEventsEventArgsTests;
    using MooVC.Architecture.Serialization;
    using MooVC.Serialization;
    using Xunit;

    public sealed class WhenDomainEventsPublishedEventArgsIsSerialized
        : DomainEventsEventArgsBase
    {
        [Fact]
        public void GivenNullEventsThenAllPropertiesAreSerialized()
        {
            var @event = new DomainEventsPublishedEventArgs(default!);
            DomainEventsPublishedAsyncEventArgs deserialized = @event.Clone();

            Assert.NotSame(@event, deserialized);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        public void GivenEventsThenAllPropertiesAreSerialized(int count)
        {
            IEnumerable<DomainEvent> events = CreateEvents(count);
            var @event = new DomainEventsPublishedEventArgs(events);

            DomainEventsPublishedAsyncEventArgs deserialized = @event.Clone();

            Assert.NotSame(@event, deserialized);
        }
    }
}