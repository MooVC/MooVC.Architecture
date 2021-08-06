namespace MooVC.Architecture.Ddd.Services.DomainEventsPublishingAsyncEventArgsTests
{
    using System.Collections.Generic;
    using MooVC.Architecture.Ddd.Services.DomainEventsAsyncEventArgsTests;
    using MooVC.Architecture.Serialization;
    using Xunit;

    public sealed class WhenDomainEventsPublishingAsyncEventArgsIsSerialized
        : DomainEventsAsyncEventArgsBase
    {
        [Fact]
        public void GivenNullEventsThenAllPropertiesAreSerialized()
        {
            var @event = new DomainEventsPublishingAsyncEventArgs(default!);
            DomainEventsPublishingAsyncEventArgs deserialized = @event.Clone();

            Assert.NotSame(@event, deserialized);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        public void GivenEventsThenAllPropertiesAreSerialized(int count)
        {
            IEnumerable<DomainEvent> events = CreateEvents(count);
            var @event = new DomainEventsPublishingAsyncEventArgs(events);

            DomainEventsPublishingAsyncEventArgs deserialized = @event.Clone();

            Assert.NotSame(@event, deserialized);
        }
    }
}