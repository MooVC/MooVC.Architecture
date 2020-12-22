namespace MooVC.Architecture.Ddd.Services.DomainEventsPublishingEventArgsTests
{
    using System.Collections.Generic;
    using MooVC.Architecture.Ddd.Services.DomainEventsEventArgsTests;
    using MooVC.Serialization;
    using Xunit;

    public sealed class WhenDomainEventsPublishingEventArgsIsSerialized
        : DomainEventsEventArgsBase
    {
        [Fact]
        public void GivenNullEventsThenAllPropertiesAreSerialized()
        {
            var @event = new DomainEventsPublishingEventArgs(default);
            DomainEventsPublishingEventArgs deserialized = @event.Clone();

            Assert.NotSame(@event, deserialized);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        public void GivenEventsThenAllPropertiesAreSerialized(int count)
        {
            IEnumerable<DomainEvent> events = CreateEvents(count);
            var @event = new DomainEventsPublishingEventArgs(events);

            DomainEventsPublishingEventArgs deserialized = @event.Clone();

            Assert.NotSame(@event, deserialized);
        }
    }
}